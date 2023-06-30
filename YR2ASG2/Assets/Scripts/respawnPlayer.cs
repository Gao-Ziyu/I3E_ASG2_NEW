/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The respawnPlayer class is used to respawn player after player dies from damage
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawnPlayer : MonoBehaviour
{
    public void RespawnPlayer()
    {
        GameManager.instance.ResetPlayer = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
