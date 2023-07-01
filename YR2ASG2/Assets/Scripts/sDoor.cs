/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The sDoors class is used for locked doors and controls
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sDoor : MonoBehaviour
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
    public Rigidbody door;
    public Collider barrier;

    /// <summary>
    /// get close & if card is collected, door will unlock if not if will not unlock
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" && capsule.cardCollected)
        {
            animation.SetBool("Open", true);
            door.isKinematic = false;
            barrier.isTrigger = true;
        }
    }

    /// <summary>
    /// door close when player exits
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            animation.SetBool("Open", false);
        }
    }
}