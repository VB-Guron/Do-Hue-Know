using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionLibrary : MonoBehaviour
{
    public AudioClip win;
    public AudioClip lose;
    public AudioClip skip;
    public AudioClip ask1;
    public AudioClip ask2;
    public AudioClip EndWinVoice1;
    public AudioClip EndWinVoice2;
    public AudioClip EndLoseVoice1;
    public AudioClip Random1;
    public AudioClip Random2;
    public AudioClip Random3;
    public AudioClip MicOff;

    public GameObject EndBanner;

    public AudioSource Sounds;

    void Start(){
        Sounds = GetComponent<AudioSource>();
    }

    public void MicOffSoundPlay(){
        Sounds.clip = MicOff;
        Sounds.Play();
    }
    public void PlayLose(){
        Sounds.clip = lose;
        Sounds.Play();
    }
    public void PlayWin(){
        Sounds.clip = win;
        Sounds.Play();
    }
    public void PlaySkip(){
        Sounds.clip = skip;
        Sounds.Play();
    }
    public void PlayAskforInput(){
        int number = Random.Range(0,2);
        if(number == 1){
            Sounds.clip = ask1;
        }else{
            Sounds.clip = ask2;
        }
        Sounds.Play();
    }

    public void PlayEndWinVoice(){
        int number = Random.Range(0,2);
        if(number == 0){
            Sounds.clip = EndWinVoice1;
        }else if(number == 1){
            Sounds.clip = EndWinVoice2;
        }
        Sounds.Play();
        EndBanner.GetComponent<EndBannerContent>().randomWinMessage(number);
    }

    public void PlayEndLoseVoice(){
        Sounds.clip = EndLoseVoice1;
        Sounds.Play();
        
        EndBanner.GetComponent<EndBannerContent>().loseMessage();
    }

    public AudioClip Level3AskQuestion(){
        int number = Random.Range(1,4);
        if(number == 1){
            Sounds.clip = Random1;
        }else if(number == 2){
            Sounds.clip = Random2;
        }else if(number ==3 ){
            Sounds.clip = Random3;
        }

        return Sounds.clip;
    }
}
