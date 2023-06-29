/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The Gun class is used for damages done by gun
 */
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [SerializeField] PlayerMovement capsule;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed = 10;

    public UnityEvent OnGunShoot;
    public float FireCooldown;

    public bool enableGun = false;

    public bool Automatic;

    private float CurrentCooldown;
    public Camera fpsCamera;

    public AudioSource shooting_sound;

    // Start is called before the first frame update
    void Start()
    {
        CurrentCooldown = FireCooldown;
        shooting_sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("gun enabled: " + enableGun);
        if (enableGun)
        {
            if (Automatic)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                if (CurrentCooldown <= 0f)
                    if (Input.GetMouseButton(0))
                    {
                        if (CurrentCooldown <= 0f)
                        {
                            OnGunShoot?.Invoke();
                            CurrentCooldown = FireCooldown;
                            shooting_sound.Play();
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
    public void Collected()
    {
        Interact();
    }

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
