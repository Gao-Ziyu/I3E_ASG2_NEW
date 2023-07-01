/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The EndGame class is used to send player back to main menu screen after the victory message.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    /// <summary>
    /// index for scene to load
    /// </summary>
    public int sceneToLoad;

    /// <summary>
    /// load game menu when player pressed return to main menu
    /// </summary>
    public void LoadMainMenu()
    {
        GameManager.instance.LoadScene(sceneToLoad = 0);
    }
}
