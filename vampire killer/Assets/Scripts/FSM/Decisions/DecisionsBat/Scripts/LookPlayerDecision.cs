using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Decision de la transicion Patrol--------->FollowPlayer
//Comprueba si el jugador entra dentro del rango de visión del GameObject

//Crea una opcion en el menu de crear asset para crear ScriptableObjects del tipo LookPlayerDecision
[CreateAssetMenu(menuName = "AIController/Decisions/LookPlayerDecision")]
public class LookPlayerDecision : Decision {

    public override bool Decide(StateController controller)
    {
        
        if (controller != null)
        {
            return Look(controller);
        }
        else
        {
            return false;
        }
    }

    private bool Look(StateController controller)
    {
        RaycastHit2D right;
        RaycastHit2D left;
        RaycastHit2D up;
        
        //Sirven para que los raycast salgan de los bordes del collider en vez del centro
        float extentsy = controller.GetComponent<Collider2D>().bounds.extents.y;
        float extentsx = controller.GetComponent<Collider2D>().bounds.extents.x;

        right = Physics2D.Raycast(new Vector2(controller.transform.position.x, controller.transform.position.y), Vector2.right, 0.3f + extentsx);
        left = Physics2D.Raycast(new Vector2(controller.transform.position.x, controller.transform.position.y), Vector2.left, 0.3f + extentsx);
        up = Physics2D.Raycast(new Vector2(controller.transform.position.x, controller.transform.position.y), Vector2.up, 0.3f + extentsy);


        if (right.collider != null)
        {

            if (right.collider.gameObject.CompareTag("Player")|| right.collider.gameObject.CompareTag("PlayerWolf"))
            {
                return true;

            }
        }
        else if (left.collider != null)
        {
            if (left.collider.gameObject.CompareTag("Player")|| left.collider.gameObject.CompareTag("PlayerWolf"))
            {
                return true;
            }

        }
        else if (up.collider != null)
        {
            if (up.collider.gameObject.CompareTag("Player")|| up.collider.gameObject.CompareTag("PlayerWolf"))
            {
                return true;
            }
        }
        return false;
    }
}
