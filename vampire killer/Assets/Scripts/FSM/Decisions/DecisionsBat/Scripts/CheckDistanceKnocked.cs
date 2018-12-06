using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Decision de la transicion KnockBack---------->GoingBack
//Comprueba la distancia entre el GameObejct y el punto de patrulla mas lejano
//Si se aleja demasiado vuelve al priemr punto de patrulla

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo CheckDistanceKnocked
[CreateAssetMenu(menuName = "AIController/Decisions/CheckDistanceKnocked")]
public class CheckDistanceKnocked : Decision {


    public override bool Decide(StateController controller)
    {
        FurthestPoints(controller);
        return CheckNearestPoint(controller);
    }

    //Comprueba la distancia entre el GameObject y el punto de patrulla mas lejano. Si se leja demasiado devuelve true sino false
    bool CheckNearestPoint(StateController controller)
    {
        float distance = Mathf.Abs(controller.furthestLeftPointKnocked.transform.position.x - controller.furthestRightPointKnocked.transform.position.x) + controller.enemyStats.MaxDistance;
        float distanceLeft = Mathf.Abs(controller.furthestLeftPointKnocked.transform.position.x - controller.transform.position.x);
        float distanceRight = Mathf.Abs(controller.furthestRightPointKnocked.transform.position.x - controller.transform.position.x);

        if ((distanceLeft > distance || distanceRight > distance)&&!controller.enemyStats.IsKnockedBack)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    //Almacena cuales son los puntos patrulla mas lejanos entre si
    void FurthestPoints(StateController controller)
    {
        controller.furthestLeftPointKnocked = controller.enemyStats.PatrolPoints[0];
        controller.furthestRightPointKnocked = controller.enemyStats.PatrolPoints[0];
        for (int i = 0; i < controller.enemyStats.PatrolPoints.Length; ++i)
        {
            if (controller.furthestLeftPointKnocked.transform.position.x > controller.enemyStats.PatrolPoints[i].transform.position.x)
            {
                controller.furthestLeftPointKnocked = controller.enemyStats.PatrolPoints[i];

            }
            if (controller.furthestRightPointKnocked.transform.position.x < controller.enemyStats.PatrolPoints[i].transform.position.x)
            {
                controller.furthestRightPointKnocked = controller.enemyStats.PatrolPoints[i];
            }
        }
    }
}
