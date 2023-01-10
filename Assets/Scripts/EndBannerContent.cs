using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndBannerContent : MonoBehaviour
{
    [Header("Declare Messages")]
    public GameObject win1;
    public GameObject win2;
    public GameObject lose1;


    [Header("Stars")]
    public GameObject stars;

    [Header("External Assets Needed for Star Calculation")]
    public GameObject Health;
    public GameObject GameScoreMonitor;


    void Start(){
        win1.SetActive(false);
        win2.SetActive(false);
        lose1.SetActive(false);
    }

    public void randomWinMessage(int number){
        if(number == 0){
            win1.SetActive(true);
        }else if(number == 1){
            win2.SetActive(true);
        }
    }
    public void loseMessage(){
        lose1.SetActive(true);
    }

    public void starsCalculation(float number){
        stars.GetComponent<Image>().fillAmount = number;
    }
}




