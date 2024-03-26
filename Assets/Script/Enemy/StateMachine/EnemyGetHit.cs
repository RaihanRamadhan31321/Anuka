using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : EnemyBaseState
{
    public override void StartState(EnemyStateManager state)
    {
        state.animator.SetBool("isRunning",false);
        state.animator.SetBool("isAttacking",false);
        state.animator.SetBool("isGetHit",true);
    }

    public override void UpdateState(EnemyStateManager state)
    {
        
    }
}
