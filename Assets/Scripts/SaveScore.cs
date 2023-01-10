using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScore : MonoBehaviour
{   

     [Header("Reference to Score Monitor")]
     public GameObject ScoreMonitor;


    [Header("Data to be recorded")]
    public string Origquestion;
    public string question;
    public string prev;
    public string answers;
    public string result;
    public string distance;
    public string q1;
    public string q2;
    public string q3;
    public string q4;
    public string q5;
    public string q6;


    public void UpdateQuestionNum(string ColorName){
        question = SceneManager.GetActiveScene().name + ", Q" + ScoreMonitor.GetComponent<ScoreMonitor>().numQuestions;
        Origquestion = SceneManager.GetActiveScene().name + ", Q" + ScoreMonitor.GetComponent<ScoreMonitor>().numQuestions + ", Q" + ScoreMonitor.GetComponent<ScoreMonitor>().numQuestions;

        PlayerPrefs.SetString(question, question);   
        PlayerPrefs.SetString(Origquestion, Origquestion);   
    }

    public void UpdateAnswerRecords(string playerAnswer,string colorName ,string playerDistance){  
        answers += PlayerPrefs.GetString(question) + ", " + playerAnswer ;
        PlayerPrefs.SetString(question, answers);
        
        if(prev != colorName){
            result = "";
        }
        
        prev = colorName;
        result += colorName + ", " + playerDistance + ", ";
        PlayerPrefs.SetString(Origquestion, result);

        distance = playerDistance;

        

    }

    public void UpdateSavedData(){
        answers = "";
        if(ScoreMonitor.GetComponent<ScoreMonitor>().numQuestions == 1){
            q1 = PlayerPrefs.GetString(question);
        }else if(ScoreMonitor.GetComponent<ScoreMonitor>().numQuestions == 2){
            q2 = PlayerPrefs.GetString(question);
        }else if(ScoreMonitor.GetComponent<ScoreMonitor>().numQuestions == 3){
            q3 = PlayerPrefs.GetString(question);
        }else if(ScoreMonitor.GetComponent<ScoreMonitor>().numQuestions == 4){
            q4 = PlayerPrefs.GetString(question);
        }else if(ScoreMonitor.GetComponent<ScoreMonitor>().numQuestions == 5){
            q5 = PlayerPrefs.GetString(question);
        }else if(ScoreMonitor.GetComponent<ScoreMonitor>().numQuestions == 6){
            q6 = PlayerPrefs.GetString(question);
        }
    }

    public void clearMemory(){
        question = "";
        answers = "";
        PlayerPrefs.DeleteAll();
    }

     public void saveData(){
        PlayerPrefs.Save();
    }
}
