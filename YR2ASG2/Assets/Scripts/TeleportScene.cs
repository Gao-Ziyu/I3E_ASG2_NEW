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
    [SerializeField] PlayerMovement capsule;
    public string scenename;

    public void Teleport()
    {
        SceneManager.LoadScene(scenename);
    }

    public void Interact()
    {
        if (scenename == "Lab")
        {
            Teleport();
        }
        else if (scenename == "SampleScene")
        {
            if (capsule.labcardCollected && capsule.toolboxCollected)
            {
                Teleport();
            }
        }
    }
}
