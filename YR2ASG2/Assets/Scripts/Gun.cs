/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The Gun class is used for damages done by gun
 */
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using static PlayerMovement;

public class Gun : MonoBehaviour, IInteractable
{
    /// <summary>
    /// get player from player script
    /// </summary>
    [SerializeField] PlayerMovement capsule;

    /// <summary>
    /// set bullet spawnpoint
    /// </summary>
    public Transform bulletSpawnPoint;

    /// <summary>
    /// set bullet for the gun
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// set bullet speed
    /// </summary>
    public float bulletSpeed = 10;

    /// <summary>
    /// shoot event
    /// </summary>
    public UnityEvent OnGunShoot;

    /// <summary>
    /// gun shoot cool down
    /// </summary>
    public float FireCooldown;

    /// <summary>
    /// gun won't shoot when it is not collected
    /// </summary>
    public bool enableGun = false;
    public bool Automatic;

    /// <summary>
    /// current shoot cooldown
    /// </summary>
    private float CurrentCooldown;

    /// <summary>
    /// gun fps camera for raycast
    /// </summary>
    public Camera fpsCamera;

    /// <summary>
    /// gun shoot audio
    /// </summary>
    public AudioSource shooting_sound;

    /// <summary>
    /// OnShoot, when left click the gun will shoot out bullets and deal damage to the enemies
    /// </summary>
    public void OnShoot()
    {
        
        if (enableGun)
        {
            if (Automatic)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                shooting_sound.Play();
                if (CurrentCooldown <= 0f)
                    if (Input.GetMouseButton(0))
                    {
                        if (CurrentCooldown <= 0f)
                        {
                            OnGunShoot?.Invoke();
                            CurrentCooldown = FireCooldown;
                        }
                    }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (CurrentCooldown <= 0f)
                    {
                        OnGunShoot?.Invoke();
                        CurrentCooldown = FireCooldown;
                    }
                }
            }
            CurrentCooldown -= Time.deltaTime;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentCooldown = FireCooldown;
        shooting_sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ///<summary>
        /// raycast masklayer so bullet can shoot through
        /// </summary>
        int layerMask = 1 << 7;
    }
    /// <summary>
    /// pick up gun with E
    /// </summary>
    public void Collected()
    {
        Interact();
    }

    /// <summary>
    /// interact with the gun by pressing E to pick up and be able to shoot
    /// </summary>
    public void Interact()
    {
        Debug.Log("gun collected");
        enableGun = true;
        capsule.gunCollected = true;
        Destroy(gameObject);
    }

    public void Enable()
    {
        enableGun = true;
    }
}