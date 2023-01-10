using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;
using TMPro;

public class StartRecorderScript : MonoBehaviour
{
    int frequency = 44100;
    [SerializeField] int numberOfSecondsToRecord = 2;
    [SerializeField] AudioSource audioSource;
    //[SerializeField] TextMeshProUGUI instructions;
    [SerializeField] string nameOfFile;
    //timer
    [SerializeField] float countdown;
    float giveToCountdown = 1;
    bool timerOn = false;
    public GameObject GameManager;
    public GameObject SoundFXLib;
    
    public float count;



	// Start is called before the first frame update
	void Start()
    {
        if(PlayerPrefs.HasKey("counterFile")){
            count = PlayerPrefs.GetFloat("counterFile");
        }else{
            count = 0;
        }
    }
	private void Update()
	{
       
        if (timerOn)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
//                instructions.text = "Recording in " + countdown.ToString("F0") + "...";
            }
            else
            {
                countdown = 0;
                timerOn = false;
 //               instructions.text = "Speak Now!";
            }
        }
        
    }


	public void StartRecordButton()
	{
        StartCoroutine(RecodVoice());
    }

    IEnumerator RecodVoice()
    {
        audioSource.Stop();
        timerOn = true;
        countdown = giveToCountdown;
        yield return new WaitForSecondsRealtime(countdown);
        audioSource.clip = Microphone.Start(Microphone.devices[0], false, numberOfSecondsToRecord, frequency);
        Debug.Log("Start Recording");
        yield return new WaitForSecondsRealtime(numberOfSecondsToRecord);
        if (audioSource != null)
        {
            SavWav.Save(Application.streamingAssetsPath + "/Final_DTW_Thesis" + "/" + nameOfFile + ".wav", audioSource.clip);
            Debug.Log("Stop Recording");
            Microphone.End(Microphone.devices[0]);
            //instructions.text = "Recording done~";
            Debug.Log("Recording done~");

            SoundFXLib.GetComponent<InstructionLibrary>().MicOffSoundPlay();
            GameManager.GetComponent<GameManager>().analyzeAnswer = true;

        }
    }
}
