using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void StartState(EnemyStateManager state);
    public abstract void UpdateState(EnemyStateManager state);
}
