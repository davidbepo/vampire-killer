using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIController/Decisions/DecisionIsAttacking")]
public class DecisionIsAttacking : Decision {



    public override bool Decide(StateController controller)
    {
        return checkIsAttacking(controller);
    }

    bool checkIsAttacking(StateController controller)
    {
        return controller.enemyStats.IsAttacking;
    }
}
