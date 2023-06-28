using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static PlayerMovement;

public class card : MonoBehaviour, IInteractable
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
    void IdleComplete()
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

        Debug.Log("card collected");
        capsule.cardCollected = true;
        Destroy(gameObject);
    }
}