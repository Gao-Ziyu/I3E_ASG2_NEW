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

public class AutoDoors : MonoBehaviour
{
    [SerializeField] PlayerMovement capsule;
    public bool isAuto;
    public Animator animation;

    //collide to open door
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            animation.SetBool("Open",true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            animation.SetBool("Open", false);
        }
    }
}
