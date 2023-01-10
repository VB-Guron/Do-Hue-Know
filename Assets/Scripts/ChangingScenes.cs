using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingScenes : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene (name);
	}
	public void Exit()
	{
		Application.Quit ();
	}
    
    
     public void LoadMainMenu()
     {  
         SceneManager.LoadScene(0);
     }
     public void LoadSelectLevel()
     {  
         SceneManager.LoadScene(1);
     }
    public void LoadGameProper()
    {  
        SceneManager.LoadScene(2);
    }
    public void LoadFinalScore(){
        SceneManager.LoadScene(5);
    }
    
    public void LoadNextScene(){
        //increment scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel2(){
        SceneManager.LoadScene("Lv2");
    }public void LoadLevel1(){
        SceneManager.LoadScene("Lv1");
    }public void LoadLevel3(){
        SceneManager.LoadScene("Lv3");
    }
    
}
