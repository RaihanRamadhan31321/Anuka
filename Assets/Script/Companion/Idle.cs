using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public bool noEnemyInSight;
    public bool playerStop;
    public Chase chaseEnemy;
    public Follow follow;


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
        state.animator.SetBool("isRunning", false);
        state.rb.velocity = Vector3.zero;
    }

    public override void UpdateState(StateManager state)
    {
        if(Vector2.Distance(state.compMovement.transform.position, state.player.transform.position + state.jarak) >= 2)//if far from player
        {
            state.SwitchState(state.followState);
        }
        if (state.compVision.enemies.Count > 0)// if chase
        {
            state.SwitchState(state.chaseState);
        }
        if (state.compMovement.isDead)//if dead
        {
            state.SwitchState(state.deathState);
        }
    }
}
