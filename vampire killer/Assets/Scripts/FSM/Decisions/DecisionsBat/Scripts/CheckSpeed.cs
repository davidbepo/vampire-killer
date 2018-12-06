using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo CheckSpeed
[CreateAssetMenu(menuName = "AIController/Decisions/CheckSpeed")]
public class CheckSpeed : Decision {

    // Use this for initialization
    public override bool Decide(StateController controller)
    {
        return speed(controller);
    }

    public bool speed(StateController controller)
    {
        if (controller.enemyStats.FollowSpeed >= 12f)
        {
            controller.enemyStats.FollowSpeed = 12f;
            controller.enemyStats.IsKnockedBack = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
