/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The Teleport class is used for teleporting between scenes
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScene : MonoBehaviour
{
    public string scenename;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            SceneManager.LoadScene(scenename);
            Debug.Log("teleport");
        }

    }
}
