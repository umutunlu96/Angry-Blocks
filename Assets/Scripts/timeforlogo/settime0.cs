using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settime0 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Time.timeScale = 1;

        }
    }
}
