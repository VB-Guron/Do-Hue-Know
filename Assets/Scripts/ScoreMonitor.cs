using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMonitor : MonoBehaviour
{
    
    [Header("Record of Answers")]
    public float countCorrect;
    public float countWrong;
    public float skipped;

    [Header("Question Monitor")]
    public float numQuestions;

    [Header("Needed Data")]
    public float star;


    public void correctAdd(){
        countCorrect++;
    }

    public void skippedPressed(){
        skipped++;
    }
    public float calculateScore(){
        if(SceneManager.GetActiveScene().name != "Lv3"){
            star = 1f - (countWrong * 0.11f) - (skipped * 0.33f);
        }else if(SceneManager.GetActiveScene().name == "Lv3"){
            star = countCorrect/(numQuestions + countWrong + skipped);
        }

        return star;
    }

    public void questionCountUp(){
        numQuestions++;
    }
}
