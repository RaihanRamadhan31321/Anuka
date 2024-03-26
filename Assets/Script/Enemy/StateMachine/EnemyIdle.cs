using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyBaseState
{
    
    public override void StartState(EnemyStateManager state)
    {
        state.animator.SetBool("isRunning",false);
        state.animator.SetBool("isAttacking",false);
        state.animator.SetBool("isGetHit",false);
        state.rb.velocity = Vector3.zero;
    }

    public override void UpdateState(EnemyStateManager state)
    {
        if (state.enemyManager.currentHealth > 0)//if not dead
        {
            if (!state.enemyManager.getHit)
            {
                if (!state.isWaiting)
                {
                    if (Vector2.Distance(state.playerManager.transform.position, state.enemyManager.transform.position + state.jarak) > 0.5f)//kalau jarak jauh dari player cek jika masuk range 
                    {
                        state.SwitchState(state.s_Chase);
                    }
                    if (Vector2.Distance(state.playerManager.transform.position, state.enemyManager.transform.position + state.jarak) <= 0.5f)//attack
                    {
                        state.SwitchState(state.s_Attack);
                    }
                }
                else
                {
                    state.StartWaitingCounter(state.waitingTime);
                }
            }
            else
            {
                state.SwitchState(state.s_GetHit);
            }
            
        }
        else
        {
            state.SwitchState(state.s_Death);
        }
        
        
    }
}
