using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class keyBoard : MonoBehaviour
{
    
    public UnityEvent GoIn;
    public UnityEvent GoOut;
    public UnityEvent Test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            GoIn.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            GoOut.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            Test.Invoke();
        }
    }
}
