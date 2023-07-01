/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The bullet class is used for bullets object that is shoot out from guns
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    /// <summary>
    /// on collision to floor the bullet will destroy
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "player")
        {
            
        }
        if (collision.gameObject.tag == "floor")
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //raycast layermask so bullet shoot through
        int layerMask = 1 << 7;
        layerMask = ~layerMask;
    }
}
