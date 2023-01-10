using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Randomizer2 : MonoBehaviour
{
    public Text questionText;
    public Text playerResult;
    public GameObject nextLevelButton;
    public GameObject pressToSpeakButton;
    int count=0;

    
    private static List<string> questionList = new List<string>() {"This is the color Yellow",
                                                                        "This is the color Orange",
                                                                        "This is the color Violet"};

    private static List<string> unansweredQuestions = new List<string>();
    private string currentQuestion;                                   
    int index;
   
    void Awake(){

    }
    void Start(){
        
        //nextLevelButton.SetActive(false); //makes next level button invisible

        if (unansweredQuestions == null || unansweredQuestions.Count == 0){
            unansweredQuestions = questionList;
        }
        
        getRandomQuestion(); //runs randomizer method 
        Debug.Log(">> CURRENT QUESTION IS : "   + currentQuestion);
        Debug.Log(">> QUESTION REMAINING : "    + unansweredQuestions.Count);
    }
    void Update(){
        //Debug.Log("QUESTION #" + count +" ||| "+ currentQuestion);
        if(count==4){
            
            questionText.gameObject.SetActive(false); //make text insivislbe
            nextLevelButton.SetActive(true); //makes next level button visible
            pressToSpeakButton.SetActive(false); //makes speaker button invisible
        }
    }
    public void getRandomQuestion(){ 
        count++;
        //for start of level
        int randomIndex = Random.Range(0,unansweredQuestions.Count); 
        currentQuestion = unansweredQuestions[randomIndex];
        unansweredQuestions.RemoveAt(randomIndex); //removes question from list
        questionText.text = currentQuestion; //set current question as text in screen
    }
    
    public void getNewQuestion(){
        count++;
        if(count!=4){
            int randomIndex = Random.Range(0,unansweredQuestions.Count); 
            currentQuestion = unansweredQuestions[randomIndex];
            unansweredQuestions.RemoveAt(randomIndex); //removes question from list
            StartCoroutine(delaySwitch()); //delays for 3 seconds b4 showing the next question
        }
        else{
            Debug.Log("No questions remaining");
        }
    }


    IEnumerator delaySwitch()
    {
        
        yield return new WaitForSeconds(3); //Wait for 3 seconds
        playerResult.gameObject.SetActive(false);//make player result text invisible
        questionText.text = currentQuestion; //set current question as text in screen
    }
}
