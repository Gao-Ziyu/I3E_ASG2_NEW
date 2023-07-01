/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The AutoDoors class is used for door triggers & animations
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

public class AutoDoors : MonoBehaviour
{
    /// <summary>
    /// get player from player script
    /// </summary>
    [SerializeField] PlayerMovement capsule;

    /// <summary>
    /// auto door animation
    /// </summary>
    public bool isAuto;
    public Animator animation;

    /// <summary>
    /// when player get close, the door will open automatically
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            animation.SetBool("Open",true);
        }
    }

    /// <summary>
    /// when player leaves, the door will close automatically
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            animation.SetBool("Open", false);
        }
    }
}
