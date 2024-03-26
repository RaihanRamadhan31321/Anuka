using UnityEngine;

public class BossChase : BossState
{

    public override void StartState(BossManager state)
    {
        
        state.animator.SetBool("isRunning", true);
        if(state.bossRG.characters.Count > 1)
        {
            switch (UnityEngine.Random.Range(1, 3))
            {
                case 1:
                    state.bossRG.enemyFlw = state.bossRG.player;
                    break;
                case 2:
                    if (BossManager.instance != null)
                    {
                        if (!BossManager.instance.bossMV.isDead)
                        {

                            state.bossRG.enemyFlw = BossManager.instance.bossMV.gameObject;
                        }
                        else
                        {
                            state.bossRG.enemyFlw = state.bossRG.player;
                        }
                    }
                    else
                    {
                        state.bossRG.enemyFlw = state.bossRG.player;
                    }
                    break;
            }
        }
        else
        {
            state.bossRG.enemyFlw = state.bossRG.player;
        }
        
    }

    public override void UpdateState(BossManager state)
    {
        state.bossMV.enemyAnimator.SetBool("isAttacking", false);
        state.bossRG.enemy.transform.position = Vector2.MoveTowards(state.bossRG.enemy.transform.position, state.bossRG.enemyFlw.transform.position + state.jarak, state.bossRG.enemy.moveSpeed * Time.deltaTime);
        if (state.bossAT.GetComponent<CircleCollider2D>().IsTouching(state.bossRG.enemyFlw.GetComponent<Collider2D>()))
        {
            if (!state.cd)
            {
                state.SwitchState(state.specialAttackState);
            }
            else
            {
                state.SwitchState(state.attackState);
            }
        }
        if(state.bossRG.characters.Count == 0)
        {
            state.SwitchState(state.idleState);
        }
        if (state.bossMV.isDead)//if dead
        {
            state.SwitchState(state.deathState);
        }
        if (!state.cd)
        {
            state.SwitchState(state.specialAttackState);
        }

    }
}
