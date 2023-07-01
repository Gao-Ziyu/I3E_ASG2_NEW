/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The PlayerMovement class is used for player controls and movement purposes
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.UI; //image
using static UnityEngine.InputSystem.DefaultInputActions;
using System.Data;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// set dialogue
    /// </summary>
    public Dialogue dialogue;

    /// <summary>
    /// get the doors from auto doors script
    /// </summary>
    [SerializeField] AutoDoors doors;

    /// <summary>
    /// player movement
    /// </summary>
    Vector3 movementInput = Vector3.zero;
    public float movementSpeed = 2f;

    /// <summary>
    /// player rotation
    /// </summary>
    Vector3 rotationInput = Vector3.zero;
    public float rotationSpeed = 1f;

    /// <summary>
    /// camera reference
    /// </summary>
    public Transform cameraTransform;

    public int value = 0;

    /// <summary>
    /// player camera
    /// </summary>
    public Transform camera;

    /// <summary>
    /// player jump force
    /// </summary>
    public float jumpForce = 10f;

    /// <summary>
    /// groundcheck for jump
    /// </summary>
    public bool groundCheck = true;

    /// <summary>
    /// sprint bar & sprinting
    /// </summary>
    public Image chargeBar;
    public bool sprinting = false;

    /// <summary>
    /// health system
    /// </summary>
    public int maxHealth = 100;
    public int currentHealth;

    /// <summary>
    /// player healthbar
    /// </summary>
    public HealthBar healthbar;

    /// <summary>
    /// check if the items are collected in order to proceed
    /// </summary>
    public bool cardCollected = false;
    public bool gunCollected = false;
    public bool labcardCollected = false;
    public bool toolboxCollected = false;

    /// <summary>
    /// gun & crosshair
    /// </summary>
    public GameObject PlayerAttack;
    public GameObject PlayerAim;

    Vector3 headRotationInput;

    /// <summary>
    /// check if the player is dead or alive
    /// </summary>
    public bool deadge = false;

    /// <summary>
    /// raycast interaction
    /// </summary>
    float interactionDistance = 3f;
    float interactionRange = 3f;
    
    /// <summary>
    /// respawn & pause menus
    /// </summary>
    public GameObject respawn;
    public GameObject pause;

    /// <summary>
    /// player death audio
    /// </summary>
    public AudioSource death;

    /// <summary>
    /// interact with objects
    /// </summary>
    public interface IInteractable
    {
        public void Interact();
    }

    /// <summary>
    /// dont destroy player on load
    /// </summary>
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// when collided on floor tag, player can jump and groundcheck
    /// when collided on door tag the door animation plays
    /// when collided on map tag a dialogue appears
    /// when collided on enemybullet tag, player minus health
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            groundCheck = true;
        }
        if (collision.gameObject.tag == "Door")
        {
            GetComponent<Animator>().SetTrigger("Open");
        }
        if (collision.gameObject.tag == "map")
        {
            dialogue.MapDialogue();
        }
        if (collision.gameObject.tag == "enemybullet")
        {
            TakeDamage(2);
        }
    }

    /// <summary>
    /// when player stays on the lava tag, will take damage overtime
    /// </summary>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("lava"))
        {
            TakeDamage(1);
        }
    }

    /// <summary>
    /// player head rotation
    /// </summary>
    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x; //look left n right
        rotationInput.x = value.Get<Vector2>().y * -1; //look up and down
    }

    /// <summary>
    /// Event called when WASD is pressed
    /// </summary>
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    /// <summary>
    /// Event called when left click on mouse
    /// </summary>
    void OnClick()
    {
        if (gunCollected == true)
        {
            PlayerAttack.GetComponent<Gun>().OnShoot();
        }
    }

    /// <summary>
    /// Event called when Spacebar is pressed
    /// </summary>
    void OnJump(InputValue value) 
    {
        if (groundCheck)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
            groundCheck = false;
        }

    }

    /// <summary>
    /// Event called when left shift is pressed
    /// </summary>
    void OnSprint(InputValue value)
    {
        // charge bar more than 0 player can sprint and charge bar will decrease
        if (chargeBar.fillAmount > 0)
        {
            movementSpeed = 7f;
            sprinting = true;
        }
        else
        {
            movementSpeed = 2f;
        }
    }

    /// <summary>
    /// Event called when escape key is pressed
    /// </summary>
    void OnEscape()
    {
        pause.SetActive(true);
    }

    /// <summary>
    /// event called when E key is pressed
    /// </summary>
    void OnInteract(InputValue value)
    {
        Ray r = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * interactionRange, Color.red, 1.0f);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactionRange))
        {
            // call the interact function on the interct object
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();
                if (hitInfo.collider.gameObject.tag == "gun")
                {
                    gunCollected = true;
                    interactObj.Interact();
                    PlayerAttack.SetActive(true);
                    PlayerAim.SetActive(true);
                    PlayerAttack.GetComponent<Gun>().Enable();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //set stamina to max
        chargeBar.fillAmount = 1;
        //set health to max
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        //hide gun & crosshair on start
        PlayerAttack.SetActive(false);
        PlayerAim.SetActive(false);

        //hide respawn & pause menus on start
        respawn.SetActive(false);
        pause.SetActive(false);

        //reset player inventory
        gunCollected = false;
        cardCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 7;

        if (deadge) return;
        //create a new vector3
        Vector3 movementVector = Vector3.zero;

        //Add the forward direction of the player mutliplied by the user's up/down input
        movementVector += transform.forward * movementInput.y;

        //Add the right direction of the player multiplied bu the user's right/left input
        movementVector += transform.right * movementInput.x;

        //Apply the movement vector multiplied by movement speed to the player's position
        transform.position += movementVector * movementSpeed * Time.deltaTime;

        //Apply the rotation mutiplied by the rotation speed
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationInput * rotationSpeed * Time.deltaTime);
        //camera.transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles + headRotationInput * rotationSpeed * Time.deltaTime);


        //make sure player dont move when camera is moving / rotating
        float rotationAmount = rotationInput.y * rotationSpeed;
        transform.Rotate(Vector3.up, rotationAmount);

        //look up and down
        //Mathf.Clamp to clamp the camera so the player does not look up and down all the way behind
        var headRot = camera.rotation.eulerAngles + new Vector3(Mathf.Clamp(rotationInput.x, -80f, 80f), 0, 0)
            * rotationSpeed;
        camera.rotation = Quaternion.Euler(headRot);

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, interactionDistance))
        {

        }



        //when shift is pressed, the chargebar will decrease
        //when shift is released, the chargebar will increase
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            // Shift key is released
            // when released, bar will charge up
            movementSpeed = 2f;
            sprinting = false;
        }

        if (sprinting)
        {
            chargeBar.fillAmount -= 0.5f * Time.deltaTime;
            if (chargeBar.fillAmount <= 0)
            {
                sprinting = false;
                movementSpeed = 2f;
            }
        }
        else
        {
            chargeBar.fillAmount += 0.5f * Time.deltaTime;
        }
    }

    /// <summary>
    /// TakeDamage & death trigger for player
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            GetComponent<Animator>().SetTrigger("dead");
            death.Play();
            deadge = true;
            Debug.Log("i die u win");
            respawn.SetActive(true);

        }
    }
}
