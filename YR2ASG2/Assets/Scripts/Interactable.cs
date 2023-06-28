/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Interact();
}
public class Interactable : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    void Start()
    {

    }

    void Update()
    {
        //check if player is pressing the E key
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            Debug.DrawRay(InteractorSource.position, InteractorSource.forward * InteractRange, Color.red, 1.0f);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log("Hit : " + hitInfo.transform.gameObject.name);
                // call the interact function on the interct object
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                    Debug.Log("Interacted with " + hitInfo.collider.gameObject.name);
                }
                
                
            }
        }
    }

}*/
