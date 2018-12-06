using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AIController/Actions/ActionAttack")]

public class ActionAttack : Action {



    public override void Act(StateController controller)
    {
        
        if (!controller.enemyStats.IsAttacking)
        {
            controller.enemyStats.GetComponent<Animator>().SetBool("isAttacking", true);
            controller.enemyStats.AttackHitbox.SetActive(true);
            controller.enemyStats.AttackHitbox.GetComponent<sEnemyHitbox>().Damage = controller.enemyStats.Damage;
            controller.enemyStats.IsAttacking = true;
        }
        if (controller.timerActionAttack >= 0.5f)
        {
            controller.enemyStats.IsAttacking = false;
            controller.enemyStats.GetComponent<Animator>().SetBool("isAttacking", false);
            controller.timerActionAttack = 0f;
        }
        if (controller.enemyStats.IsAttacking)
        {
            controller.timerActionAttack += 1 * Time.deltaTime;
        }
    }

}
