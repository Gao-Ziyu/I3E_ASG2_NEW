/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The PlayerMovement class is used for player controls and movement purposes
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayer : MonoBehaviour
{
    /// <summary>
    /// check player in range
    /// </summary>
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// when player is detected, will turn player in range = true and enemy will chase & attack
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            playerInRange = true;
        }
    }

    /// <summary>
    /// when player not detected or left the range, enemy will patrol
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            playerInRange = false;
        }
    }
}
