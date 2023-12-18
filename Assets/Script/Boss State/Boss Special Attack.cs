using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialAttack : BossState
{
    public float distance;
    public bool isWaiting = false;
    
    public override void StartState(BossManager state)
    {
        state.animator.SetBool("isCharging", true);
        state.isCharging = false;

        state.rb.velocity = Vector3.zero;
        state.bossRG.enemyFlw = state.player;
        state.lineRenderer.SetPosition(0, state.gameObject.transform.position);
        state.lineRenderer.SetPosition(1, state.bossRG.enemyFlw.transform.position);

    }

    public override void UpdateState(BossManager state)
    {
        distance = Vector2.Distance(state.transform.position, state.bossRG.enemyFlw.transform.position);
        if (!state.isCharging)
        {
            state.lineRenderer.SetPosition(0, state.gameObject.transform.position);
            state.lineRenderer.SetPosition(1, state.bossRG.enemyFlw.transform.position);
            if (!isWaiting)
            {
                isWaiting = true;
                state.WaitBeforeCharge();
            }
            
        }
        else
        {
            
            state.lineRenderer.SetPosition(0, state.gameObject.transform.position);
            state.bossRG.enemy.transform.position = Vector2.MoveTowards(state.bossRG.enemy.transform.position, state.chargeTarget, (state.bossRG.enemy.moveSpeed * 10) * Time.deltaTime);
        }
        
        
        if(Vector2.Distance(state.gameObject.transform.position, state.lineRenderer.GetPosition(1)) < 1.5)
        {
            state.animator.SetBool("isCharging", false);
            state.cd = true;
            
            state.lineRenderer.SetPosition(0, Vector3.zero);
            state.lineRenderer.SetPosition(1, Vector3.zero);
            state.SpecialAttackOnCD();
            state.SwitchState(state.idleState);
        }
    }
}
