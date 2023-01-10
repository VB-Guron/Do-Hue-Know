using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image Healthbar;

    public float health;
    

    private void Start(){
        Healthbar.fillAmount = health / 10;
    }


    private void Update(){
        Healthbar.fillAmount = health / 10;
    }

    public void Decrease(int number){
        health = health - number;
    }
}
