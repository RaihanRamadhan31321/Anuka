using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossState
{

    public override void StartState(BossManager state)
    {
        
        state.animator.SetBool("isRunning", false);
        state.rb.velocity = Vector3.zero;
    }

    public override void UpdateState(BossManager state)
    {
        state.bossMV.enemyAnimator.SetBool("isAttacking", false);
        if (state.bossRG.characters.Count > 0)// if chase
        {
            state.SwitchState(state.chaseState);
        }
        if (state.bossMV.isDead)//if dead
        {
            state.SwitchState(state.deathState);
        }
    }
}
