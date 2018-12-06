using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIController/Actions/ActionBossSpawn")]
public class ActionBossSpawn : Action {

    float timer=0f;
    //float projectileTimer = 0f;
    float count=0f;
    EnemyBoss boss;

    public override void Act(StateController controller) {
        if (boss == null)
            boss = (EnemyBoss)controller.enemyStats;
        if (CheckInPlace(controller))
            Spawn(controller);
        else
            MoveToPoint(controller);
    }

    public void Spawn(StateController controller) {
        if (count >= 3) {
            boss.isSpawning = false;
            boss.isShooting = true;
            count = 0f;
        }
        if (timer >= 2f) {
            Instantiate(boss.Minion, boss.MinionSpawnPoint.transform.position, boss.gameObject.transform.rotation);
            ++count;
            timer = 0f;
        }
        timer += 1 * Time.deltaTime;
    }

    bool CheckInPlace(StateController controller) {
        return  ((Mathf.Abs(boss.SpawnPoint.transform.position.x - controller.transform.position.x) < 0.5f) &&
            (Mathf.Abs(boss.SpawnPoint.transform.position.y - controller.transform.position.y) < 0.5f));
    }

    void MoveToPoint(StateController controller) {
        float directionx = Mathf.Sign(boss.SpawnPoint.transform.position.x - controller.transform.position.x);
        float directiony = Mathf.Sign(boss.SpawnPoint.transform.position.y - controller.transform.position.y);
        controller.transform.position = new Vector2(controller.transform.position.x + boss.FollowSpeed * Time.deltaTime * directionx,
            controller.transform.position.y + boss.FollowSpeed * Time.deltaTime * directiony);
    }

}
