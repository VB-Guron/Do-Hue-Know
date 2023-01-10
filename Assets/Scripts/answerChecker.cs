using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class answerChecker : MonoBehaviour
{
    public Text playerResult;
    public Text playerFinalResult;
    int currentScene;
    int Score;
    
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject); //keeps script alive for all scenes
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //insert code to use python algo

        //if algo returns unknown or unclear/repeat question
        //repeatAnswer();
        
        //if algo returns correct
        //correctAnswer();

        //if algo returns incorrect
        //wrongAnswer();
    }

    
    ///////////////////////
    //NOT YET OPERATIONAL//
    ///////////////////////

    //Method to keep track of score
    public void endScore(){
        //for victory screen messages
        playerResult.text = ("Your score is: " + Score.ToString() + "/3"); //display total score

        //display message based on score
        if(Score==3){
            playerFinalResult.text = "You Did Well!";
        }
        else{
            playerFinalResult.text = "Practice Makes Perfect!";
        }
    }


    //////////////////////////////////////
    //  TEMPORARY METHODS FOR TESTING   //
    //////////////////////////////////////
    public void repeatAnswer(){
        //if algo cant detect answer
        playerResult.text = "Sorry, Can you repeat that?"; 
    }

    public void correctAnswer(){
        playerResult.text ="Correct! Lets move on to the next color."; 
        Score++;
    }
    
    public void wrongAnswer(){
        playerResult.text = "Lets try a different color, shall we?"; 
    }
}
