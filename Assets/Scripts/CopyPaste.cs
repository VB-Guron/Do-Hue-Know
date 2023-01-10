using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class CopyPaste : MonoBehaviour
{
     Process myProcess;
      private static GameObject sampleInstance;
        private void Awake()
        {
            Process[] myProcesses = Process.GetProcessesByName("server");
                    foreach (Process p in myProcesses){
                        UnityEngine.Debug.Log(p);
                    }
            if (sampleInstance != null){
                Destroy(sampleInstance);
            }else{
                sampleInstance = gameObject;
                DontDestroyOnLoad(this);

                //string path;
                //path = Application.streamingAssetsPath+ "/Final_DTW_Thesis/dist/server/server.exe";
                myProcess = new Process();
                myProcess.StartInfo.FileName = Application.streamingAssetsPath + "/Final_DTW_Thesis/dist/server/server.exe";
                //myProcess.StartInfo.Arguments = path;
                myProcess.Start();
            }

            
        }
    // Start is called before the first frame update
    void Start()
    {

      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit(){
        Process[] myProcesses = Process.GetProcessesByName("server");
       foreach (Process p in myProcesses)
        {
            p.Kill();
            p.CloseMainWindow(); 
            p.WaitForExit();
            p.Dispose();
        }
    }
}

