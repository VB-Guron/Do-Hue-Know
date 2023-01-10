using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class WriteToCSV : MonoBehaviour
{
    public string fileName;
    public string fileName2;
    public float playerNumber;
    public GameObject name, level1_Q1, level1_Q2, level1_Q3, level2_Q1, level2_Q2, level2_Q3, level3_Q1, level3_Q2, level3_Q3, level3_Q4, level3_Q5, level3_Q6, timeLevel1, timeLevel2, timeLevel3, extra, extra2;

    
    public List<GameObject> LevelRecord = new List<GameObject>();
    public List<GameObject> TimeRecord = new List<GameObject>();

    public GameObject TimeManager;

    public int counter;
    public int counterTime;
    // Start is called before the first frame update
    void Start()
    {
        fileName = Application.persistentDataPath + "/data2.csv";
        fileName =  Application.streamingAssetsPath  + "/data2.csv";
        LevelRecord.Add(level1_Q1);
        LevelRecord.Add(level1_Q2);
        LevelRecord.Add(level1_Q3);
        LevelRecord.Add(level2_Q1);
        LevelRecord.Add(level2_Q2);
        LevelRecord.Add(level2_Q3);
        LevelRecord.Add(level3_Q1);
        LevelRecord.Add(level3_Q2);
        LevelRecord.Add(level3_Q3);
        LevelRecord.Add(level3_Q4);
        LevelRecord.Add(level3_Q5);
        LevelRecord.Add(level3_Q6);

        TimeRecord.Add(timeLevel1);
        TimeRecord.Add(timeLevel2);
        TimeRecord.Add(timeLevel3);
        counter = 0;
        counterTime = 0;

        if(!PlayerPrefs.HasKey("PlayerNumber")){
            PlayerPrefs.SetFloat("PlayerNumber", playerNumber);
        }
        ShowData();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowData(){
        counter = 0;
        counterTime = 0;
        name.GetComponent<TextMeshProUGUI>().text = "Player " + PlayerPrefs.GetFloat("PlayerNumber");

        for(int i = 1; i <= 2; i++){
            for(int j = 1; j <= 3; j++){
                Debug.Log("Loop : lv" + i + " Q" + j + "counter: " +counter);
                if(PlayerPrefs.HasKey("Lv" + i + ", Q" + j)){
                    LevelRecord[counter].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv" + i + ", Q" + j);
                    
                    counter++;
                }else{
                    LevelRecord[counter].GetComponent<TextMeshProUGUI>().text = "Lv" + i + ", Q" + j;
                    counter++;

                }
            }
        }
        for(int i = 3; i <= 3; i++){
            for(int j = 1; j <= 6; j++){
                Debug.Log("Loop : lv" + i + " Q" + j + "counter: " +counter);
                if(PlayerPrefs.HasKey("Lv" + i + ", Q" + j)){
                    LevelRecord[counter].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv" + i + ", Q" + j);
                    
                    counter++;
                }else{
                    LevelRecord[counter].GetComponent<TextMeshProUGUI>().text = "Lv" + i + ", Q" + j;
                    counter++;

                }
            }
        }

        for(int i = 1; i<=3; i++){
            if(PlayerPrefs.HasKey("Lv" + i + " Time")){
                TimeRecord[counterTime].GetComponent<TextMeshProUGUI>().text = "Time: " + PlayerPrefs.GetString("Lv" + i + " Time");
                
                counterTime++;
            }else{
                TimeRecord[counterTime].GetComponent<TextMeshProUGUI>().text = "Lv" + i + " Time: No Record";
                counterTime++;
            }
        }
        /*
        if(PlayerPrefs.HasKey("Lv1, Q1")){
            level1_Q1.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv1, Q1");
            level1_Q2.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv1, Q2");
            level1_Q3.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv1, Q3");
        }
        if(PlayerPrefs.HasKey("Lv2, Q1")){
            level2_Q1.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv2, Q1");
            level2_Q2.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv2, Q2");
            level2_Q3.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv2, Q3");
        }

        if(PlayerPrefs.HasKey("Lv3, Q1")){
            level3_Q1.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv3, Q1");
            level3_Q2.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv3, Q2");
            level3_Q3.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv3, Q3");
            level3_Q4.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv3, Q4");
            level3_Q5.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv3, Q5");
            level3_Q6.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Lv3, Q6");
        }
        */
    }

    public void WriteDataToCSV(){
        TextWriter tw = new StreamWriter(fileName,true);
        for(int i = 1; i<=3; i++){
            if(PlayerPrefs.HasKey("Lv" + i + " Time")){
                tw.WriteLine(PlayerPrefs.GetFloat("PlayerNumber") + ", , , , , ," + PlayerPrefs.GetString("Lv" + i + " Time"));
                
            }else{
                tw.WriteLine(PlayerPrefs.GetFloat("PlayerNumber") + ", , , , , ," + "Lv" + i + " Time: No Record");
            }
        }
        
        
        for(int i = 1; i <= 2; i++){
            for(int j = 1; j <= 3; j++){
                Debug.Log("Loop : lv" + i + " Q" + j + "counter: " +counter);
                if(PlayerPrefs.HasKey("Lv" + i + ", Q" + j)){
                    tw.WriteLine(PlayerPrefs.GetFloat("PlayerNumber")+", " + PlayerPrefs.GetString("Lv" + i + ", Q" + j));
                    // tw.WriteLine(" , , , , , , " + PlayerPrefs.GetString("Lv" + i + ", Q" + j + ", Q" + j));  
                    
                }else{
                    tw.WriteLine(PlayerPrefs.GetFloat("PlayerNumber")+", " + "Lv" + i + ", Q" + j + ", No Data");  

                }
            }
        }
        for(int i = 3; i <= 3; i++){
            for(int j = 1; j <= 6; j++){
                Debug.Log("Loop : lv" + i + " Q" + j + "counter: " +counter);
                if(PlayerPrefs.HasKey("Lv" + i + ", Q" + j)){
                    tw.WriteLine(PlayerPrefs.GetFloat("PlayerNumber")+", " + PlayerPrefs.GetString("Lv" + i + ", Q" + j));  
                    
                }else{
                    tw.WriteLine(PlayerPrefs.GetFloat("PlayerNumber")+", " + "Lv" + i + ", Q" + j +", No Data");  
                }
            }
        }

        




        tw.Close();

        UpdatePlayerNumber();
        clear();
        ShowData();
        
    }


    public void UpdatePlayerNumber(){
        playerNumber++;
        PlayerPrefs.SetFloat("PlayerNumber",playerNumber);
        name.GetComponent<TextMeshProUGUI>().text = "Player " + PlayerPrefs.GetFloat("PlayerNumber");
    }

    public void DecreasePlayerNumber(){
        playerNumber--;
        PlayerPrefs.SetFloat("PlayerNumber",playerNumber);
        name.GetComponent<TextMeshProUGUI>().text = "Player " + PlayerPrefs.GetFloat("PlayerNumber");
    }

    public void clear(){
        playerNumber =  PlayerPrefs.GetFloat("PlayerNumber");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("PlayerNumber",playerNumber);
        ShowData();
    }

   
}
