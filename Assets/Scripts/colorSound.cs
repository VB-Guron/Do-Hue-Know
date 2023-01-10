using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class colorSound : MonoBehaviour
{   

    public AudioSource sounds;
    public GameObject Library;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<AudioSource>();
    }


    public void playSound(){
        if(SceneManager.GetActiveScene().name != "Lv3"){
            sounds.Play();
        }else if(SceneManager.GetActiveScene().name == "Lv3"){
            sounds.clip = Library.GetComponent<InstructionLibrary>().Level3AskQuestion();
            sounds.Play();

        }
    }

}
