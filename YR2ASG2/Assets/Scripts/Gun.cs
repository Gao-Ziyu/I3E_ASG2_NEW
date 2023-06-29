/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The Gun class is used for gun controls
 */
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [SerializeField] PlayerMovement capsule;
    public UnityEvent OnGunShoot;
    public float FireCooldown;

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
        if (Automatic)
        {
            if (Input.GetMouseButton(0))
            {
                if(CurrentCooldown <= 0f)
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
                if(CurrentCooldown <= 0f)
                {
                    OnGunShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
        }


        CurrentCooldown -= Time.deltaTime;
    }
    public void Collected()
    {
        Interact();
    }

    public void Interact()
    {

        Debug.Log("gun collected");
        capsule.gunCollected = true;
        Destroy(gameObject);
    }
}
