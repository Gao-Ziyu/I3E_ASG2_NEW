/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The bullet class is used for bullets object that is shoot out from guns
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Destroy(gameObject);
            Debug.Log("touch me");
        }
    }
}
