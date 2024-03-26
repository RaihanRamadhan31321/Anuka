using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtk : EnemyBaseState

{
    public override void StartState(EnemyStateManager state)
    {
        state.animator.SetBool("isRunning",false);
        state.animator.SetBool("isAttacking",true);
        state.animator.SetBool("isGetHit",false);
    }

    public override void UpdateState(EnemyStateManager state)
    {
        
    }
}
