using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int localCounter = 0;
    static int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        localCounter++;
        counter++;
        Debug.Log("counter value: " + counter + " " + localCounter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Collected()
    {
        //play animation
        //audio to play when collected/
        Destroy(gameObject);
    }
}
