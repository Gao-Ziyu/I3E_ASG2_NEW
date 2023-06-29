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
    //load the next level
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //quit game
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
