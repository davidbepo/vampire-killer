using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIController/Decision/DecisionBossIsSpawning")]
public class DecisionBossisSpawning : Decision {

    EnemyBoss boss;

    public override bool Decide(StateController controller)
    {
        if (boss == null)
        {
            boss = (EnemyBoss)controller.enemyStats;
        }
        if(!boss.isSpawning)
        Debug.Log(boss.isSpawning);
        return boss.isSpawning;
    }
}
