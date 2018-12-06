using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Decision de la transicion FollowPlayer-------->GoingBack
//Comprueba la distancia entre el GameObject y el punto de patrulla mas lejano.
//Si se aleja demasiado vuelve al primer punto de patrulla

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo CheckDistance
[CreateAssetMenu(menuName = "AIController/Decisions/CheckDistance")]
public class CheckDistance : Decision {



    public override bool Decide(StateController controller)
    {
        furthestPoints(controller);
        return CheckDistanceFunction(controller);
    }
    
    //Comprueba la distancia entre el GameObject y el punto de patrulla mas lejano. Si se leja demasiado devuelve true sino false
    bool CheckDistanceFunction(StateController controller)
    {

        float distance = Mathf.Abs(controller.furthestLeftPoint.transform.position.x - controller.furthestRightPoint.transform.position.x) + controller.enemyStats.MaxDistance;
        float distanceLeft = Mathf.Abs(controller.furthestLeftPoint.transform.position.x - controller.transform.position.x);
        float distanceRight = Mathf.Abs(controller.furthestRightPoint.transform.position.x - controller.transform.position.x);

        if (distanceLeft > distance || distanceRight > distance)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    //Almacena cuales son los puntos patrulla mas lejanos entre si
    void furthestPoints(StateController controller)
    {
        controller.furthestLeftPoint = controller.enemyStats.PatrolPoints[0];
        controller.furthestRightPoint = controller.enemyStats.PatrolPoints[0];
        for (int i = 0; i < controller.enemyStats.PatrolPoints.Length; ++i)
        {
            if (controller.furthestLeftPoint.transform.position.x > controller.enemyStats.PatrolPoints[i].transform.position.x)
            {
                controller.furthestLeftPoint = controller.enemyStats.PatrolPoints[i];

            }
            if (controller.furthestRightPoint.transform.position.x < controller.enemyStats.PatrolPoints[i].transform.position.x)
            {
                controller.furthestRightPoint = controller.enemyStats.PatrolPoints[i];
            }
        }
    }
}
