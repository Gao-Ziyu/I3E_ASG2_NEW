using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
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
}
