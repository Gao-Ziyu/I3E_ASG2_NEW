/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The final class is used for final interaction to display out the victory message
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class final : MonoBehaviour, IInteractable
{
    /// <summary>
    /// get player from player script
    /// </summary>
    [SerializeField] PlayerMovement capsule;

    /// <summary>
    /// victory message
    /// </summary>
    public GameObject victory;

    // Start is called before the first frame update
    void Start()
    {
        victory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// press E to interact to fix the wire panels and victory message will pop out
    /// </summary>
    public void Interact()
    {
        if (capsule.toolboxCollected)
        {
            victory.SetActive(true);
        }
    }
}
