/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The card class is used collectibles managing
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static PlayerMovement;

public class card : MonoBehaviour, IInteractable
{
    /// <summary>
    /// get player from player script
    /// </summary>
    [SerializeField] PlayerMovement capsule;

    /// <summary>
    /// audio played when card is collected
    /// </summary>
    public AudioSource cardCollected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// collect card
    /// </summary>
    public void Collected()
    {
        Interact();

    }

    /// <summary>
    /// press E to INTERACT with card
    /// </summary>
    public void Interact()
    {
        GetComponent<Animator>().SetTrigger("cardCollect");
        cardCollected.Play();
        capsule.cardCollected = true;
        gameObject.SetActive(false);
    }
}