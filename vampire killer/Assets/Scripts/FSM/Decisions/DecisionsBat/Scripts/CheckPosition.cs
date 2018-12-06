using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Decision de la transicion GoingBack--------->Patrol
//Comprueba si el GameObject a vuelto a su punto de patrulla objetivo antes de que fuese interrumpido

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo CheckPosition
[CreateAssetMenu(menuName = "AIController/Decisions/CheckPosition")]
public class CheckPosition : Decision {

    public override bool Decide(StateController controller)
    {
        return checkPosition(controller);
    }

    public bool checkPosition(StateController controller)
    {
        if (Mathf.Abs(controller.enemyStats.CurrentPatrol.transform.position.x - controller.transform.position.x) < 0.5f)
        {
            controller.enemyStats.Health = controller.enemyStats.MaxHealth;
            return true;
        }
        else
        {
            return false;
        }
    }

}
