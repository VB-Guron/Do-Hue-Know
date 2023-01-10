using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Initial ColorName
    [Header("Colors")]
    public string color1;
    public string color2;
    public string color3;
    public string color4;
    public string color5;
    public string color6;
    
    //Initial ColorObject
    [Header("Objects")]
    public GameObject colorObj1; 
    public GameObject colorObj2; 
    public GameObject colorObj3;
    public GameObject colorObj4;
    public GameObject colorObj5;
    public GameObject colorObj6;

    //List that will be use for Game
    [Header("Lists")]
    public List<string> ColorList = new List<string>();
    public List<string> unansweredQuestions = new List<string>();

    public List<GameObject> ColorsObject = new List<GameObject>();

    //UI Manipulation
    [Header("UI")]
    public Text colorName_Text;

    //Counter
    [Header("Control")]
    public int randomIndex;
    public int index;
    public int count=0;
    public bool getAQuestion;
    public bool longer = false;
    public bool nextQuestion;
    public bool Level3;
    public bool analyzeAnswer;

    //Current holdings
    [Header("Current Objects")]
    public string currentColorName;
    public GameObject currentColorObject;

    //Buttons for TextVersion
    [Header("Buttons")]
    public GameObject nextLevelButton;
    public GameObject pressToSpeakButton;

    //TestCounter
    private int testCounter;

    [Header("Audio")]
    public GameObject Audio;

    [Header("Animations")]
    public GameObject Speaker_Speaking;
    public GameObject SpeakingButtonBlink;

    [Header("Health")]
    public GameObject HealthMonitor;
    public float startingHealth;

    [Header("EndBanner")]
    public GameObject endBanner;
    public GameObject ScoreMonitor;
    public GameObject GameTimer;

    [Header("Monitor and Saves Score")]
    public GameObject SaveScore;

    [Header("Voice Input Controls")]
    public GameObject MicPressed;
    public GameObject VoiceAnalyzer;
    public string pythonResult;
    public string[] pythonResultsplit;
    public bool receivedResponse;


    [Header("Events")]
    public UnityEvent SpeakButton;
    public UnityEvent Correct;
    public UnityEvent Wrong;
    public UnityEvent skipButton;
    public UnityEvent DisableAllButton;
    public UnityEvent EnableAllButtons;

    


    // Start is called before the first frame update
    void Start()
    {
        HealthMonitor.GetComponent<Health>().health = startingHealth;
        testCounter = 0;
        Result(ClearAllList());
        Result(AddColorList());
        getAQuestion = true;
        endBanner.SetActive(false);

        if(SceneManager.GetActiveScene().name == "Lv3"){
            Level3 = true;
            ColorList.Add(color4);
            ColorList.Add(color5);
            ColorList.Add(color6);

            ColorsObject.Add(colorObj4);
            ColorsObject.Add(colorObj5);
            ColorsObject.Add(colorObj6);

        }else{
            Level3 = false;
        }

        analyzeAnswer = false;
        pythonResult = "";
        receivedResponse  = false;

    }
    
    // Update is called once per frame
    void Update()
    {
        //If there are still questions present it
        if(getAQuestion){
            DisableAllButton.Invoke();
            ScoreMonitor.GetComponent<ScoreMonitor>().questionCountUp();
            SaveScore.GetComponent<SaveScore>().UpdateQuestionNum(currentColorName);
            getAQuestion = false;
            //Check if there is current Question
            if(currentColorObject == null){
                PlayIconSpeaking();
                Result(GetARandomQuestion());
                StartCoroutine(AskForInput());
            }
            else{
                PlayIconSpeaking();
                Result(HideCurrentQuestion());
                StartCoroutine(pause());
            }
        }

        if(analyzeAnswer){
            //We Analyze
            Debug.Log("Call python");
            analyzeAnswer= false;
            AnalyzeVoiceInput();
        }

        if(receivedResponse){
            receivedResponse = false;
            //Debug.Log("What is the result: " + pythonResult);
            pythonResultsplit = pythonResult.Split(',');
            Debug.Log("What is the result: " + pythonResultsplit[0]);
            Debug.Log("What is the result: " + pythonResultsplit[1]);
            //PlayerPrefs.GetString("Lv" + i + ", Q" + j + ", color" + j)
            if(pythonResultsplit[0] == "same"){
                CorrectButton();
            }else if(pythonResultsplit[0] == "different"){
                WrongButton();
            }else{
                Debug.Log("Somethings Wrong Check PythonResult");
            }
        }
        

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            HealthDecrease();
        }
    }
    public void showPythonResult(){
        Debug.Log("result: " + pythonResult);
        receivedResponse = true;
    }

    public void AnalyzeVoiceInput(){
        VoiceAnalyzer.GetComponent<HelloClient>().color = currentColorName;
        VoiceAnalyzer.GetComponent<HelloClient>().Execute();

    }

    public void RecordVoice(){
        MicPressed.GetComponent<StartRecorderScript>().StartRecordButton();
    }

    public void skipButtonPressed(){
        skipButton.Invoke();
    }
    
    public void HealthDecrease(){
        HealthMonitor.GetComponent<Health>().Decrease(1);
    }
    public void pressedSpeakButton(){
        SpeakButton.Invoke();
    }
    public void CorrectButton(){
        Correct.Invoke();
    }
    public void WrongButton(){
        Wrong.Invoke();
    }
    public void PlayIconSpeaking(){
        Speaker_Speaking.GetComponent<Animator>().SetTrigger("speaking");
    }
    public void PlayIconStop(){
        Speaker_Speaking.GetComponent<Animator>().SetTrigger("silent");
    }
    public void SpeakButtonBlink(){
        SpeakingButtonBlink.GetComponent<Animator>().SetTrigger("blink");
    }
    public void SpeakButtonStopBlink(){
        SpeakingButtonBlink.GetComponent<Animator>().SetTrigger("stopBlink");
    }
    public void CorrectAnswer(){
        Audio.GetComponent<InstructionLibrary>().PlayWin();
    }
    
    IEnumerator AskForInput(){
        yield return new WaitForSeconds(3f);
        if(!Level3){
            Audio.GetComponent<InstructionLibrary>().PlayAskforInput();
        }
        yield return new WaitForSeconds(1f);
        PlayIconStop();
        SpeakButtonBlink();
        EnableAllButtons.Invoke();
    }
    IEnumerator AskForInputRepeat(){
        yield return new WaitForSeconds(1.5f); 
        if(!Level3){
            Audio.GetComponent<InstructionLibrary>().PlayAskforInput();
        }
        yield return new WaitForSeconds(1f);
        PlayIconStop();
        SpeakButtonBlink();
        EnableAllButtons.Invoke();
    }
    IEnumerator pause(){
        yield return new WaitForSeconds(1f); //Wait for 3 seconds
        Result(GetARandomQuestion());
        yield return new WaitForSeconds(1.5f); //Wait for 3 seconds
        StartCoroutine(AskForInput());
    }
    public void Repeat(){
        DisableAllButton.Invoke();
        SpeakButtonStopBlink();
        PlayIconSpeaking();
        currentColorObject.GetComponent<AudioSource>().Play();
        StartCoroutine(AskForInputRepeat());
    }
    public void Skip(){
        SpeakButtonStopBlink();
        if(unansweredQuestions.Count !=0){
            PlayIconSpeaking();
            HealthMonitor.GetComponent<Health>().health = 3;
            Audio.GetComponent<InstructionLibrary>().PlaySkip();
            getAQuestion = true;
        }else{
            EndOfLevel();
        }
    }
    public void WrongAnswer(){
        DisableAllButton.Invoke();
        HealthDecrease();
        ScoreMonitor.GetComponent<ScoreMonitor>().countWrong++;
        SaveScore.GetComponent<SaveScore>().UpdateAnswerRecords("Wrong", currentColorName, pythonResultsplit[1]);
        SaveScore.GetComponent<SaveScore>().UpdateSavedData();
        if(HealthMonitor.GetComponent<Health>().health != 0){
            PlayIconSpeaking();
            Audio.GetComponent<InstructionLibrary>().PlayLose();
            StartCoroutine(loseRepeat());
        }else{
            Skip();
            HealthMonitor.GetComponent<Health>().health = 3;
        }
        
    }
    IEnumerator loseRepeat(){
        yield return new WaitForSeconds(3f);
        currentColorObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2f); 
        if(!Level3){
            Audio.GetComponent<InstructionLibrary>().PlayAskforInput();
        }
        yield return new WaitForSeconds(1f);
        PlayIconStop();
        SpeakButtonBlink();
        EnableAllButtons.Invoke();
    }
    public void NextQuestion(){
        ScoreMonitor.GetComponent<ScoreMonitor>().correctAdd();
        SaveScore.GetComponent<SaveScore>().UpdateAnswerRecords("Correct",currentColorName,pythonResultsplit[1]);
        SaveScore.GetComponent<SaveScore>().UpdateSavedData();
        if(unansweredQuestions.Count !=0){
            HealthMonitor.GetComponent<Health>().health = 3;
            PlayIconSpeaking();
            CorrectAnswer();
            getAQuestion = true;
        }else{
            EndOfLevel();
        }
    }

    public void EndOfLevel(){
        //nextLevelButton.SetActive(true);
        GameTimer.GetComponent<GameTimer>().endTimer();
        PlayIconStop();
        EnableAllButtons.Invoke();
        endBanner.GetComponent<EndBannerContent>().starsCalculation(ScoreMonitor.GetComponent<ScoreMonitor>().calculateScore());

        if(ScoreMonitor.GetComponent<ScoreMonitor>().calculateScore() < 0.45){
            Audio.GetComponent<InstructionLibrary>().PlayEndLoseVoice();
        }else{
            Audio.GetComponent<InstructionLibrary>().PlayEndWinVoice();
        }
        endBanner.SetActive(true);
    }
    public bool HideCurrentQuestion(){
        colorName_Text.GetComponent<Animator>().SetTrigger("hide");
        currentColorObject.GetComponent<Animator>().SetTrigger("hide");

        return true;
    }
    public bool GetARandomQuestion(){
        //pick a random number
        randomIndex = Random.Range(0, unansweredQuestions.Count);

        //Get color name and object by random number
        currentColorName = unansweredQuestions[randomIndex];
        currentColorObject = ColorsObject[randomIndex];
        

        if(!longer){
            currentColorObject.GetComponent<Animator>().SetBool("firstQuestion",true);
            colorName_Text.GetComponent<Animator>().SetBool("longer", false);
            longer = true;
        }else{
            //For Next Colors
            currentColorObject.GetComponent<Animator>().SetBool("firstQuestion",false);
        }

        //Delete selected color from list
        unansweredQuestions.RemoveAt(randomIndex);
        ColorsObject.RemoveAt(randomIndex);

        //Show them in UI
        colorName_Text.text = currentColorName;
        colorName_Text.GetComponent<Animator>().SetTrigger("show");
        currentColorObject.GetComponent<Animator>().SetTrigger("show");


        return true;
    }
    public bool AddColorList(){
        ColorList.Add(color1);
        ColorList.Add(color2);
        ColorList.Add(color3);

        ColorsObject.Add(colorObj1);
        ColorsObject.Add(colorObj2);
        ColorsObject.Add(colorObj3);

        unansweredQuestions = ColorList;
        return true;
    }
    public bool ClearAllList(){
        ColorList.Clear();
        unansweredQuestions.Clear();
        ColorsObject.Clear();

        return true;
    }
    public void Result(bool result){
        testCounter++;
        if(result){
            Debug.Log("Test #" + testCounter + " passed");
        }else{
            Debug.Log("Test #" + testCounter + " failed");
        }
    }
    
}
