/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The ButtonLab class is used for button interaction in the lab
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class ButtonLab : MonoBehaviour, IInteractable
{
    /// <summary>
    /// platform for activation 
    /// </summary>
    [SerializeField] platform platform;
    [SerializeField] platform platform1;
    [SerializeField] platform platform2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// when E is pressed, the platforms will rise.
    /// </summary>
    public void Interact()
    {
        platform.Rise();
        platform1.Rise();
        platform2.Rise();
    }
}
