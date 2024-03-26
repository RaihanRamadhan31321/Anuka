using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBaseState
{
    public override void StartState(EnemyStateManager state)
    {
        state.animator.SetBool("isRunning",true);
        state.animator.SetBool("isAttacking",false);
        state.animator.SetBool("isGetHit",false);
    }

    public override void UpdateState(EnemyStateManager state)
    {
        
    }
}
