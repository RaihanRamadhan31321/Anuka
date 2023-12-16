using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    private bool cek = false;


    public override void OnTriggerEnter2D(Collider2D collision, StateManager state)
    {

    }

    public override void OnTriggerExit2D(Collider2D collision, StateManager state)
    {
        
    }

    public override void OnTriggerStay2D(Collider2D collision, StateManager state)
    {

    }

    public override void StartState(StateManager state)
    {

    }

    public override void UpdateState(StateManager state)
    {
        if (state.compAttack.colider.IsTouching(state.compVision.closestEnemy.GetComponent<Collider2D>()))
        {
            if (!state.cd)
            {
                state.animator.SetBool("isAttacking", true);
                state.compVision.closestEnemy.GetComponent<EnemyMovement>().TakeDamage(state.compAttack.attackDamage);
                state.compVision.closestEnemy.GetComponentInChildren<Enemyattack>().GetHit();

                state.cd = true;
                cek = true;
            }
            if(cek)
            {
                cek = false;
                state.AttackCooldown();
            }
        }
        else
        {
            state.SwitchState(state.followState);
        }
        
    }
    
}
