using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    private bool timerIsActive=false;
    float lvlTime; //entire play session
    int currentScene;
    string lvlTimeText;
   
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject); //keeps script alive for all scenes
        currentScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("CURRENT SCENE: " + currentScene); 
    }

    void Start()
    {
        timerIsActive=true;
    }
    void Update()
    {
        if(timerIsActive){
            lvlTime += Time.deltaTime;
            lvlTimeText = lvlTime.ToString("0.00");
        }
        
    }
    public void endTimer(){
        timerIsActive = false;
        Debug.Log("===> SCENE (" + currentScene + ") TIME: " + lvlTimeText);
        PlayerPrefs.SetString(""+ SceneManager.GetActiveScene().name + " Time" , lvlTimeText);
    }
}
