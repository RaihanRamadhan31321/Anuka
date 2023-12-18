using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    public abstract void StartState(BossManager state);
    public abstract void UpdateState(BossManager state);

}
