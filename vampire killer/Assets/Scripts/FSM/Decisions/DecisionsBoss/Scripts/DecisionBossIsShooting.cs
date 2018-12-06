using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIController/Decision/DecisionBossIsShooting")]
public class DecisionBossIsShooting : Decision {

    EnemyBoss boss;

   public override bool Decide(StateController controller)
    {
        boss = (EnemyBoss)controller.enemyStats;

        return boss.isShooting;
    }

}
