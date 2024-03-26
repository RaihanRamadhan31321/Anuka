using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : EnemyBaseState
{
    public override void StartState(EnemyStateManager state)
    {
        state.animator.SetTrigger("isDead");
        state.animator.SetBool("isRunning",false);
        state.animator.SetBool("isAttacking",false);
        state.animator.SetBool("isGetHit",false);
    }

    public override void UpdateState(EnemyStateManager state)
    {
        
    }
}
