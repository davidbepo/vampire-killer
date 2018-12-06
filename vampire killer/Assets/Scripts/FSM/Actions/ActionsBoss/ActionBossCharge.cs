using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIController/Actions/ActionBossCharge")]
public class ActionBossCharge : Action {

    float timer;
    EnemyBoss boss;

    public override void Act(StateController controller)
    {
        if (boss == null)
        {
            boss = (EnemyBoss)controller.enemyStats;
        }
        if (CheckInPlace(controller))
        {
            Charge();
        }
        else
        {
            MoveToPoint(controller);
        }

        if (timer >= 6f)
        {
            timer = 0f;
            boss.isCharging = false;
            boss.isSpawning = true;
        }
    }

    void MoveToPoint(StateController controller)
    {
        //float directionx = Mathf.Sign(boss.ChargingPoint.transform.position.x - controller.transform.position.x);
        float directiony = Mathf.Sign(boss.ChargingPoint.transform.position.y - controller.transform.position.y);
        controller.transform.position = new Vector2(controller.transform.position.x,
            controller.transform.position.y + boss.FollowSpeed * Time.deltaTime * directiony);
    }

    bool CheckInPlace(StateController controller)
    {
        if ( (Mathf.Abs(boss.ChargingPoint.transform.position.y - controller.transform.position.y) < 0.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Charge()
    {
        timer += 1 * Time.deltaTime;
    }
}
