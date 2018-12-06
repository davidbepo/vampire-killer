using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta accion se porduce cuando el gameObject se encuentra en estado empujado.

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo KnockBackAction
[CreateAssetMenu(menuName = "AIController/Actions/KnockBackAction")]
public class KnockBackAction : Action {

    //GameObject player;
    Player playerScript;
    //Sirve para controlar si el gameObject ya ha sido empujado
    bool knocked;
    float timer;
    //Tiempo que el GameObject permanecera en estado de empujado
    float timeKnocked;
    float timeRatio;

    //Funcion que se va a llamar desde el update de StateController
    public override void Act(StateController controller) {
        if (!knocked) {
            KnockBack(controller);
        }else if (Timer()){
            knocked=false;
            timer = 0f;
            controller.enemyStats.IsKnockedBack = false;
        }
    }

    public void OnEnable() {
        //player = GameObject.FindGameObjectWithTag("Player");
        knocked = false;
        timer = 0f;
        timeKnocked = 1f;
        timeRatio = 1f;
    }

    //Empuja al gameObject
    public void KnockBack(StateController controller)
    {
        controller.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        controller.GetComponent<Rigidbody2D>().AddForce(new Vector2(50f, 0f)*controller.enemyStats.KnockBackDirection, ForceMode2D.Force);
        Debug.Log("IsKnockedBack");
        controller.enemyStats.FollowSpeed = 0f;
        knocked = true;
    }

    //Temporizador
    public bool Timer(){
        timer += timeRatio *Time.deltaTime;
        if(timer>=timeKnocked){
            return true;
        }
        Debug.Log("Todavia esta knockeado");
        return false;
    }
}
