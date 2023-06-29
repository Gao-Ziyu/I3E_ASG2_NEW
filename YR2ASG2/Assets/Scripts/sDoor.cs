/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The sDoors class is used for locked doors and controls
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sDoor : MonoBehaviour
{
    [SerializeField] PlayerMovement capsule;
    public bool isAuto;
    public Animator animation;
    public Rigidbody door;
    public Collider barrier;

    //collide to open door
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" && capsule.cardCollected)
        {
            animation.SetBool("Open", true);
            door.isKinematic = false;
            barrier.isTrigger = true;
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