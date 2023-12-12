using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPOS : MonoBehaviour
{
    public static enemyPOS instance;
    public int enemyInSight;
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyInSight++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyInSight--;
        }
    }
    public void CheckSite()
    {
        if (enemyInSight < 4)
        {
            EnemyRange.instance.enemy.moving = true;
            EnemyRange.instance.enemy.transform.position = Vector2.MoveTowards(EnemyRange.instance.enemy.transform.position, EnemyRange.instance.player.transform.position, EnemyRange.instance.enemy.moveSpeed * Time.deltaTime);
        }
        else
        {
            EnemyRange.instance.enemy.moving = false;
            EnemyRange.instance.enemy.enemyAnimator.SetBool("isRunning", false);
            //FreeCheck();
        }
    }

}
