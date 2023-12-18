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

            //knock nearby char
            if(Vector2.Distance(state.transform.position, state.compManager.transform.position) < 3)
            {
                if(state.transform.position.x < state.compManager.transform.position.x)
                {
                    state.compManager.GetComponent<Rigidbody2D>().AddForce(Vector2.right * state.knockBack, ForceMode2D.Impulse);
                }
                else
                {
                    state.compManager.GetComponent<Rigidbody2D>().AddForce(Vector2.left * state.knockBack, ForceMode2D.Impulse);
                }
                    
            }

            if (!isWaiting)
            {
                isWaiting = true;
                state.WaitBeforeCharge();
            }
            
        }
        else
        {
            
            state.lineRenderer.SetPosition(0, state.gameObject.transform.position);
            state.bossRG.enemy.transform.position = Vector2.MoveTowards(state.bossRG.enemy.transform.position, state.chargeTarget, (state.bossRG.enemy.moveSpeed * 6) * Time.deltaTime);
            if (state.bossAT.GetComponent<Collider2D>().IsTouching(state.playerManager.playerMV.GetComponent<Collider2D>()))
            {
                state.playerManager.playerATK.GetHit();
                state.playerManager.playerHP.TakeDamage(5);
            }
            if (state.bossAT.GetComponent<Collider2D>().IsTouching(state.compManager.GetComponent<Collider2D>()))
            {
                state.compManager.compAttack.GetHit();
                state.compManager.compMovement.TakeDamage(5);
            }
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
