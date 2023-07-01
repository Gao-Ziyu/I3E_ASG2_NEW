/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The MainMenu class is used for main menu controls at start of the game
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //<summary>
    // set the scene to load
    //</summary>
    public int sceneToLoad;

    /// <summary>
    /// load the next level
    /// </summary>
    public void PlayGame() 
    {
        GameManager.instance.LoadScene(sceneToLoad = 1);
    }
    /// <summary>
    /// quit game
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
