using UnityEngine;
using UnityEngine.UI;

public class HelloClient : MonoBehaviour
{
    public Text txt;
    public string color;
    private HelloRequester _helloRequester;
    public GameObject GameManager;

    public void Start()
    {
       

         
         _helloRequester = new HelloRequester();
        
        
    }


    public void Update(){
        
        if(_helloRequester.reply() != null){
            GameManager.GetComponent<GameManager>().pythonResult = _helloRequester.reply();
            GameManager.GetComponent<GameManager>().showPythonResult();

            _helloRequester.clearReply();
        }
    }
    private void OnDestroy()
    {
        _helloRequester.Stop();
    }

    public void Execute(){
        
         _helloRequester = new HelloRequester();
         _helloRequester.SetColor(color);
        _helloRequester.Start();
    }
}