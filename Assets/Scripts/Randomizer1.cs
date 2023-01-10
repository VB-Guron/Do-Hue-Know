using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Randomizer1 : MonoBehaviour
{
    public string Color1;
    public string Color2;
    public string Color3;
    public Text questionText;
    public Text playerResult;
    public GameObject currentQuestionInObject;
    public GameObject nextLevelButton;
    public GameObject pressToSpeakButton;
    int count=0;
    public GameObject colorNameText;
    
    private static List<string> questionList = new List<string>();

    private static List<string> unansweredQuestions = new List<string>();
    public List<GameObject> ColorsInList = new List<GameObject>();
    private string currentQuestion;                                   
    int index;
   
    void Awake(){
        
        //Get all color objects
        //Colors = GameObject.FindGameObjectsWithTag("Color");
    }
    void Start(){
        
        
        questionList.Add(Color1);
        questionList.Add(Color2);
        questionList.Add(Color3);


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
        currentQuestionInObject = ColorsInList[randomIndex];
        
        Debug.Log(ColorsInList[randomIndex].name);

        ColorsInList.RemoveAt(randomIndex);
        unansweredQuestions.RemoveAt(randomIndex); //removes question from list

        currentQuestionInObject.GetComponent<Animator>().SetTrigger("show");
        colorNameText.GetComponent<Animator>().SetTrigger("show");
        questionText.text = currentQuestion; //set current question as text in screen
    }
    
    public void getNewQuestion(){
        count++;
        if(count!=4){
            colorNameText.GetComponent<Animator>().SetTrigger("hide");
            currentQuestionInObject.GetComponent<Animator>().SetTrigger("hide");
            int randomIndex = Random.Range(0,unansweredQuestions.Count); 
            currentQuestion = unansweredQuestions[randomIndex];
            currentQuestionInObject = ColorsInList[randomIndex];


            ColorsInList.RemoveAt(randomIndex);
            unansweredQuestions.RemoveAt(randomIndex); //removes question from list
            StartCoroutine(delaySwitch(randomIndex)); //delays for 3 seconds b4 showing the next question
        }
        else{
            Debug.Log("No questions remaining | Count : " + count);
        }
    }

    IEnumerator delaySwitch(int random)
    {
        
        currentQuestionInObject.GetComponent<Animator>().SetTrigger("show");
        playerResult.gameObject.SetActive(false);//make player result text invisible
        
        yield return new WaitForSeconds(0.5f); //Wait for 3 seconds
        
        questionText.text = currentQuestion; //set current question as text in screen
        colorNameText.GetComponent<Animator>().SetTrigger("show");
    }
}
