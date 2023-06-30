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
    [SerializeField] PlayerMovement capsule;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Collected()
    {
        GetComponent<Animator>().SetTrigger("cardCollect");
        GetComponent<AudioSource>().Play();
        Interact();

    }

    public void Interact()
    {
        Debug.Log("toolbox collected");
        capsule.toolboxCollected = true;
        Destroy(gameObject);
    }
}
