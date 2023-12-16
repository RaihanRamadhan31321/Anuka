using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : State
{

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
        state.animator.SetBool("isRunning", true);
        state.compMovement.target = state.player;
    }

    public override void UpdateState(StateManager state)
    {
        state.compMovement.transform.position = Vector2.MoveTowards(state.compMovement.transform.position, state.player.transform.position + state.jarakPlayer, state.compMovement.moveSpeed * Time.deltaTime);
        if(Vector2.Distance(state.compMovement.transform.position,  state.player.transform.position + state.jarakPlayer) < 2)
        {
            state.SwitchState(state.idleState);
        }
        if(state.compVision.enemies.Count > 0)
        {
            state.SwitchState(state.chaseState);
        }
    }
}
