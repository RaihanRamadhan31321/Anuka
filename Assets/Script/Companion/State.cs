using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class State
{
    public abstract void StartState(StateManager state);
    public abstract void UpdateState(StateManager state);
    public abstract void OnTriggerEnter2D(Collider2D collision, StateManager state);
    public abstract void OnTriggerExit2D(Collider2D collision, StateManager state);
    public abstract void OnTriggerStay2D(Collider2D collision, StateManager state);

}
