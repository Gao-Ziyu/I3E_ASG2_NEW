/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The PlayerMovement class is used for player controls and movement purposes
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayer : MonoBehaviour
{
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            playerInRange = true;
            Debug.Log("player in range");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            playerInRange = false;
        }
    }
}
