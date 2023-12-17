using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public Follow followPlayer;
    public Attack attackEnemy;
    public bool isInAttackRange;
    public bool enemyInSight;


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
        state.compMovement.target = state.compVision.closestEnemy;
    }

    public override void UpdateState(StateManager state)
    {
        if (state.compVision.closestEnemy != null)
        {
            state.compMovement.transform.position = Vector2.MoveTowards(state.compMovement.transform.position, state.compVision.closestEnemy.transform.position +state.jarak, state.compMovement.moveSpeed * Time.deltaTime);
            if (state.compAttack.colider.IsTouching(state.compVision.closestEnemy.GetComponent<Collider2D>()))
            {
                state.SwitchState(state.attackState);
            }
        }
        else
        {
            state.SwitchState(state.followState);
        }
        
        if(state.compVision.enemies.Count == 0)
        {
            state.SwitchState(state.followState);
        }
        if (state.compMovement.isDead)//if dead
        {
            state.SwitchState(state.deathState);
        }
    }
}
