/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The labcard class is used to interact with the card collectible at lab
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class labcard : MonoBehaviour, IInteractable
{
    /// <summary>
    /// get player from player script
    /// </summary>
    [SerializeField] PlayerMovement capsule;

    /// <summary>
    /// audio when labcard is collected
    /// </summary>
    public AudioSource labcardCollected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// collect lab card
    /// </summary>
    public void Collected()
    {
        Interact();
    }

    /// <summary>
    /// when E is pressed, lab card will be collected
    /// </summary>
    public void Interact()
    {
        GetComponent<Animator>().SetTrigger("cardCollect");
        labcardCollected.Play();
        capsule.labcardCollected = true;
        Destroy(gameObject);
    }
}
