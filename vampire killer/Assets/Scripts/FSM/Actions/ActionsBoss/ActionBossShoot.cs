using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIController/Actions/ActionBossShoot")]
public class ActionBossShoot : Action {

    float timer=0f;
    float shootingTimer = 0f;
    EnemyBoss boss;
    float count = 0f;
    GameObject currentPatrol;

    public override void Act(StateController controller)
    {
        if (boss == null)
        {
            boss = (EnemyBoss)controller.enemyStats;
            currentPatrol = boss.ShootingPoints[2];
        }

        if (CheckInPlace(controller))
        {
            Shoot(controller);
        }
        else
        {
            MoveToPoint(controller);
        }

        if (count >= 3)
        {
            count = 0f;
            boss.isShooting = false;
            boss.isCharging = true;
        }
    }

    void Shoot(StateController controller)
    {
        if (shootingTimer >=0.5f)
        {
            GameObject clone = Instantiate(boss.Projectile, boss.MinionSpawnPoint.transform.position, boss.gameObject.transform.rotation);
            clone.GetComponent<Rigidbody2D>().simulated = true;
            clone.GetComponent<Rigidbody2D>().velocity =  new Vector2(Random.Range(-10f, 10f), clone.GetComponent<Rigidbody2D>().velocity.y);
            shootingTimer = 0f;
        }

        if (timer >= 4f)
        {
            timer = 0f;
            ChangePatrol(controller);
            ++count;
        }
        timer += 1 * Time.deltaTime;
        shootingTimer += 1 * Time.deltaTime;
    }

    void MoveToPoint(StateController controller)
    {
        
        float directionx = Mathf.Sign(currentPatrol.transform.position.x - controller.transform.position.x);
        float directiony = Mathf.Sign(currentPatrol.transform.position.y - controller.transform.position.y);
        controller.transform.position = new Vector2(controller.transform.position.x + boss.FollowSpeed * Time.deltaTime * directionx,
            controller.transform.position.y + boss.FollowSpeed * Time.deltaTime * directiony);
    }

    bool CheckInPlace(StateController controller)
    {
        if(Mathf.Abs(currentPatrol.transform.position.x - controller.transform.position.x) < 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ChangePatrol(StateController controller)
    {
        GameObject player = controller.enemyStats.Player;
        float currentDistance;
        float minDistance;

        minDistance = Mathf.Abs(player.transform.position.x - boss.ShootingPoints[0].transform.position.x);
        currentPatrol = boss.ShootingPoints[0];
        for(int i=0; i<boss.ShootingPoints.Length; ++i)
        {
            currentDistance = Mathf.Abs(player.transform.position.x - boss.ShootingPoints[i].transform.position.x);
            if (currentDistance < minDistance)
            {
                currentPatrol = boss.ShootingPoints[i];
            }
        }
    }


}
