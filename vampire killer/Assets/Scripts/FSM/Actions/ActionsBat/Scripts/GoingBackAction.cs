using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Accion que se produce cuando el gameObject esta en estado de GoingBack. El gameObject vuelve a un punto de patrulla.
//Mientras vuelve es invulnerable a cualquier ataque y regenera vida

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo GoingBackAction
[CreateAssetMenu(menuName = "AIController/Actions/GoingBackAction")]
public class GoingBackAction : Action {



    //Funcion que se va a llamar desde el update de StateController
    public override void Act(StateController controller)
    {
        if(controller.enemyStats.FollowSpeed>=controller.enemyStats.MaxSpeed){
        goBack(controller);
        }else{
            SpeedUp(controller);
        }
    }

    public void goBack(StateController controller)
    {
        if (controller.enemyStats.Health < controller.enemyStats.MaxHealth)
        {
            controller.enemyStats.Health += 10f * Time.deltaTime;
        }
        float direction = Mathf.Sign(controller.enemyStats.CurrentPatrol.transform.position.x - controller.transform.position.x);
        controller.transform.position = new Vector2(controller.transform.position.x + controller.enemyStats.FollowSpeed * Time.deltaTime * direction, controller.transform.position.y);
    }

    //Aumenta el atributo followSpeed del StateController introducido como parametro en funcion del atributo acceleration del mismo StateController
    public void SpeedUp(StateController controller)
    {
        controller.enemyStats.FollowSpeed += Time.deltaTime * controller.enemyStats.Acceleration;
        float direction = Mathf.Sign(controller.enemyStats.CurrentPatrol.transform.position.x - controller.transform.position.x);
        controller.transform.position = new Vector2(controller.transform.position.x + controller.enemyStats.FollowSpeed * Time.deltaTime * direction, controller.transform.position.y);
    }

}
