using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIController/Actions/ActionMinionFollowPlayer")]
public class ActionBossMinionFollowPlayer : Action {


    public override void Act(StateController controller) {
        if (controller != null)
            followPlayer(controller);
    }


    void followPlayer(StateController controller) {
        Debug.Log(controller.ToString());
            float directionx = Mathf.Sign(controller.enemyStats.Player.transform.position.x - controller.transform.position.x);
            float directiony = Mathf.Sign(controller.enemyStats.Player.transform.position.y - controller.transform.position.y);
            controller.transform.position = new Vector2(controller.transform.position.x + controller.enemyStats.FollowSpeed * Time.deltaTime * directionx,
                controller.transform.position.y + controller.enemyStats.FollowSpeed * Time.deltaTime * directiony);

    }
}
