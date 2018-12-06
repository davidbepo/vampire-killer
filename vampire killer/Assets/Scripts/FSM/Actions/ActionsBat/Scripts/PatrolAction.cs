using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta accion se porduce cuando el gameObject se encuentra en estado patrullando. El GameObject patrullara entre unos puntos definidos

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo PatrolAction
[CreateAssetMenu(menuName = "AIController/Actions/Patrol")]
public class PatrolAction : Action {


    //Funcion que se va a llamar desde el update de StateController
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    void Patrol(StateController controller)
    {
        controller.enemyStats.CurrentPatrol = controller.enemyStats.PatrolPoints[controller.patrolCount % controller.enemyStats.PatrolPoints.Length];
        //Si el GameObject a llegado a su destino cambia su punto objetivo al siguiente del array
        if (Mathf.Abs(controller.enemyStats.CurrentPatrol.transform.position.x - controller.transform.position.x) < 0.5f)
        {
            controller.enemyStats.CurrentPatrol = controller.enemyStats.PatrolPoints[controller.patrolCount % controller.enemyStats.PatrolPoints.Length];
            ++controller.patrolCount;
        }
        float direction = Mathf.Sign(controller.enemyStats.CurrentPatrol.transform.position.x - controller.transform.position.x);
        controller.transform.position = new Vector2(controller.transform.position.x + controller.enemyStats.FollowSpeed * Time.deltaTime * direction, controller.transform.position.y);
    }
}
