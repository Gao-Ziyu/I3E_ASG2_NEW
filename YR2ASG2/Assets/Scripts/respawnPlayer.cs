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
    /// <summary>
    /// respawn player when respawn button is pressed
    /// </summary>
    public void RespawnPlayer()
    {
        GameManager.instance.ResetPlayer = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// quit game when quit is pressed
    /// </summary>
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
