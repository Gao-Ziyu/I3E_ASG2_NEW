/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The platform class is used for activate the platforms from lab to get across the lava
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// call the animation after the button is pressed so the platform will rise from the lava
    /// </summary>
    public void Rise()
    {
        GetComponent<Animator>().SetTrigger("Activate");
    }
}
