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
    public Dialogue dialogue;
    [SerializeField] AutoDoors doors;
    //public int lab;
    //public GameObject camera;
    //player movement
    Vector3 movementInput = Vector3.zero;
    public float movementSpeed = 0.07f;
    //rotation
    Vector3 rotationInput = Vector3.zero;
    public float rotationSpeed = 1f;
    // Camera reference
    public Transform cameraTransform;
    int score = 0;
    public int value = 0;
    //camera
    public Transform camera;
    //jump
    public float jumpForce = 10f;
    //ground check
    public bool groundCheck = true;
    //sprintbar
    public Image chargeBar;
    public bool sprinting = false;

    // health system
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    public bool cardCollected = false;
    public bool gunCollected = false;

    public GameObject PlayerAttack;
    public GameObject PlayerAim;

    Vector3 headRotationInput;

    public bool deadge = false;

    float interactionDistance = 3f;
    float interactionRange = 3f;

    public interface IInteractable
    {
        public void Interact();
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
        if (collision.gameObject.tag == "lava")
        {
            deadge = true;
            Debug.Log("i die u win");
        }
    }

    /*public void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }*/

    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x; //look left n right
        rotationInput.x = value.Get<Vector2>().y * -1; //look up and down
    }
    void OnMove(InputValue value) //Event called when WASD is pressed
    {
        movementInput = value.Get<Vector2>();
    }

    void OnClick()
    {

    }

    void OnJump(InputValue value) //Event called when Spacebar is pressed
    {
        if (groundCheck)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
            groundCheck = false;
        }

    }

    void OnSprint(InputValue value) //Event called when left shift is pressed
    {
        // charge bar more than 0 player can sprint and charge bar will decrease
        if (chargeBar.fillAmount > 0)
        {
            movementSpeed = 7f;
            sprinting = true;
        }
        else
        {
            movementSpeed = 5f;
        }
    }

    void OnInteract(InputValue value) // when E pressed, interact
    {
        Ray r = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * interactionRange, Color.red, 1.0f);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactionRange))
        {
            Debug.Log("Hit : " + hitInfo.transform.gameObject.name);
            // call the interact function on the interct object
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();
                Debug.Log("Interacted with " + hitInfo.collider.gameObject.name);

                if (hitInfo.collider.gameObject.tag == "gun")
                {
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
        PlayerAttack.SetActive(false);
        PlayerAim.SetActive(false);

        gunCollected = false;
        cardCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (deadge) return;
        /*Vector3 forwardDir = transform.forward; //Object forward action

        forwardDir *= movementInput.y;          // W/S keyboard input 1 / 0 / -1 (y value)

        Vector3 rightDir = transform.right;     // Object right action

        rightDir *= movementInput.x;            // A/D Keyboard input 1 / 0 / -1 (x value)

        GetComponent<Rigidbody>().MovePosition(transform.position +
            (forwardDir + rightDir) * movementSpeed);*/


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
            Debug.Log(hitInfo.transform.name);
        }



        //when shift is pressed, the chargebar will decrease
        //when shift is released, the chargebar will increase
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            // Shift key is released
            // when released, bar will charge up
            movementSpeed = 7f;
            sprinting = false;
        }

        if (sprinting)
        {
            chargeBar.fillAmount -= 0.5f * Time.deltaTime;
            if (chargeBar.fillAmount <= 0)
            {
                sprinting = false;
                movementSpeed = 5f;
            }
        }
        else
        {
            chargeBar.fillAmount += 0.5f * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20);
        }

        //pick gun up
        /*if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("gun picked");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "gun")
            {
                Debug.Log("pick up gun");
                PlayerAttack.SetActive(true);
                PlayerAim.SetActive(true);

            }
        }*/

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            GetComponent<Animator>().SetTrigger("dead");
            deadge = true;
            Debug.Log("i die u win");

        }
    }
}
