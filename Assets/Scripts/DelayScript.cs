using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayScript : MonoBehaviour
{
    public void startDelay(){
        StartCoroutine(delaySwitch());
    }
    
    IEnumerator delaySwitch()
    {
        //Randomizer3.getRandomQuestion();
        //Wait for 3 seconds
        yield return new WaitForSeconds(3);
        
    }
    
}
