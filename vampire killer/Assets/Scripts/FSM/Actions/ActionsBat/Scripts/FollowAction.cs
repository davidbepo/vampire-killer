using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Accion que se produce cuando el gameObject se encuentra en estado de persecucion. El gameObject persigue al jugador

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo FollowAction
[CreateAssetMenu(menuName = "AIController/Actions/FollowAction")]

public class FollowAction : Action {


    //Funcion que se va a llamar desde el update de StateController
    public override void Act(StateController controller)
    {
        if(controller.enemyStats.FollowSpeed>=12f){
        followPlayer(controller);
        }else {
            SpeedUp(controller);
        }
    }

    void followPlayer(StateController controller)
    {

        float direction = Mathf.Sign(controller.enemyStats.Player.transform.position.x - controller.transform.position.x);
        controller.transform.position = new Vector2(controller.transform.position.x + controller.enemyStats.FollowSpeed * Time.deltaTime * direction, controller.transform.position.y);

    }

     //Aumenta el atributo followSpeed del StateController introducido como parametro en funcion del atributo acceleration del mismo StateController
        public void SpeedUp(StateController controller)
    {
        controller.enemyStats.FollowSpeed += Time.deltaTime * controller.enemyStats.Acceleration;
        float direction = Mathf.Sign(controller.enemyStats.Player.transform.position.x - controller.transform.position.x);
        controller.transform.position = new Vector2(controller.transform.position.x + controller.enemyStats.FollowSpeed * Time.deltaTime * direction, controller.transform.position.y);
    }

    //Comprueba si se choca con el jugador. Si se chocan el gameobject y el jugador reciben un empujon(knockBack) 
    void checkPlayer(StateController controller){
        RaycastHit2D right;
        RaycastHit2D left;
        RaycastHit2D up;

        //Sirve para que los raycast salgan de los bordes del collider en vez del centro
        float extentsy = controller.GetComponent<Collider2D>().bounds.extents.y;
        float extentsx = controller.GetComponent<Collider2D>().bounds.extents.x;

        right = Physics2D.Raycast(new Vector2(controller.transform.position.x, controller.transform.position.y), Vector2.right, 1f + extentsx);
        left = Physics2D.Raycast(new Vector2(controller.transform.position.x, controller.transform.position.y), Vector2.left, 1f + extentsx);
        up = Physics2D.Raycast(new Vector2(controller.transform.position.x, controller.transform.position.y), Vector2.up, 1f + extentsy);


        if (right.collider != null)
        {
            if (right.collider.gameObject.CompareTag("Player")|| right.collider.gameObject.CompareTag("PlayerWolf"))
            {
                //Daña al jugador
                gameController.instance.takeDamage(controller.enemyStats.Damage);
                //Sirve para cambiar de estado a KnockedBack        
                controller.enemyStats.IsKnockedBack = true;
                //Indica la direccion en la que el gameObject va a ser empujado
                controller.enemyStats.KnockBackDirection = Vector2.left;
                //Empuja al jugador en la direccion pasada como parametro
                controller.enemyStats.Player.GetComponent<Player>().knockBack(Vector2.left);

            }
        }
        else if (left.collider != null)
        {
            if (left.collider.gameObject.CompareTag("Player")|| left.collider.gameObject.CompareTag("PlayerWolf"))
            {
                gameController.instance.takeDamage(controller.enemyStats.Damage);
                controller.enemyStats.IsKnockedBack = true;
               controller.enemyStats.KnockBackDirection = Vector2.right;
                controller.enemyStats.Player.GetComponent<Player>().knockBack(Vector2.left);
            }

        }
        else if (up.collider != null)
        {
            if (up.collider.gameObject.CompareTag("Player") || up.collider.gameObject.CompareTag("PlayerWolf"))
            {
                gameController.instance.takeDamage(controller.enemyStats.Damage);
               controller.enemyStats.Player.GetComponent<Player>().knockBack(Vector2.up);
            }
        }
    }
}
