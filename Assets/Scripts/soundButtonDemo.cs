using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundButtonDemo : MonoBehaviour
{
    public AudioSource sound;
    public Animator anim;
    public Button but;



    public void playDelayedSoundEnd(){
        anim.SetTrigger("stopBlink");
        but.interactable = false;
        StartCoroutine(playDelayedSound());
    }


    IEnumerator playDelayedSound(){
        yield return new WaitForSeconds(3);
        sound.Play();
        but.interactable = true;
        anim.SetTrigger("blink");
    }
}
