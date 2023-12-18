using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : BossState
{
    public override void StartState(BossManager state)
    {
        state.animator.SetBool("isRunning", false);

    }

    public override void UpdateState(BossManager state)
    {
        
        if (!state.bossAT.GetComponent<CircleCollider2D>().IsTouching(state.bossRG.enemyFlw.GetComponent<Collider2D>()))
        {
            state.SwitchState(state.chaseState);
        }
        else
        {
            if (state.bossAT.cd)
            {
                state.bossAT.CooldownBasicAttack();
            }
            if (state.bossAT.CanAttack)
            {
                state.bossAT.BasicAttack();
            }
        }
        if(!state.cd)
        {
            state.SwitchState(state.specialAttackState);
        }
    }
}
