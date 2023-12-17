using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : State
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
        state.DeathCounter(20);
    }

    public override void UpdateState(StateManager state)
    {
        if(!state.compMovement.isDead)
        {

            state.SwitchState(state.idleState);
        }
    }
}
