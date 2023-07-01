/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The toolbox class is used to interact with the toolbox collectible at lab
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class toolbox : MonoBehaviour, IInteractable
{
    /// <summary>
    /// get player from player script
    /// </summary>
    [SerializeField] PlayerMovement capsule;

    /// <summary>
    /// audio played when toolbox is collected
    /// </summary>
    public AudioSource toolboxCollected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// collect toolbox
    /// </summary>
    public void Collected()
    {
        Interact();
    }

    /// <summary>
    /// interact with toolbox by pressing E
    /// </summary>
    public void Interact()
    {
        toolboxCollected.Play();
        Debug.Log("toolbox collected");
        capsule.toolboxCollected = true;
        Destroy(gameObject);
    }
}
