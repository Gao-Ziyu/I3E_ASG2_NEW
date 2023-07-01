/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The Teleport class is used for teleporting between scenes
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerMovement;

public class TeleportScene : MonoBehaviour, IInteractable
{
    /// <summary>
    /// get player from player script
    /// </summary>
    [SerializeField] PlayerMovement capsule;

    /// <summary>
    /// enter the scene index to load scene of choice
    /// </summary>
    public int sceneToLoad;

    /// <summary>
    /// teleport with a fade transition
    /// </summary>
    public void Teleport()
    {
        GameManager.instance.LoadScene(sceneToLoad);
    }

    /// <summary>
    /// press E to interact and teleport to the next scene
    /// </summary>
    public void Interact()
    {
        if (sceneToLoad == 2)
        {
            Teleport();
        }
        else if (sceneToLoad == 1)
        {
            if (capsule.labcardCollected && capsule.toolboxCollected)
            {
                Teleport();
            }
        }
    }
}
