/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The labdoorLock class is used for locked warehouse door in 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labdoorLock : MonoBehaviour
{
    /// <summary>
    /// get player from player script
    /// </summary>
    [SerializeField] PlayerMovement capsule;

    /// <summary>
    /// door animation
    /// </summary>
    public bool isAuto;
    public Animator animation;
    public Rigidbody door;

    /// <summary>
    /// barrier collider to stop player from going through the door when card is not collected
    /// </summary>
    public Collider barrier;

    /// <summary>
    /// when player does not have the lab card, they cannot go through there will be a barrier stopping them and the door will not open
    /// the door will unlock and player will be able to access it after getting the card.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" && capsule.labcardCollected)
        {
            animation.SetBool("Open", true);
            door.isKinematic = false;
            barrier.isTrigger = true;
        }
    }

    /// <summary>
    /// after player enters the room the door will auto close behind them
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            animation.SetBool("Open", false);
        }
    }
}