using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Decision de la transicion FollowPlayer---------->KnockBack
//Comprueba si el GameObject ha sido empujado

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo CheckKnockBack
[CreateAssetMenu(menuName = "AIController/Decisions/CheckKnockBack")]
public class CheckKnockBack : Decision {

    // Use this for initialization
    public override bool Decide(StateController controller)
    {
        if (controller.enemyStats.IsKnockedBack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
