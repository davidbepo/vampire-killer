using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIController/Decision/DecisionBossIsCharging")]
public class DecisionBossIsCharging : Decision {

    EnemyBoss boss;

    public override bool Decide(StateController controller)
    {
        if(boss == null)
        {
            boss = (EnemyBoss)controller.enemyStats;
        }
        return boss.isCharging;
    }
}
