using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    private EnemyMovement enemy;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject.GetComponent<EnemyMovement>();
        if (PlayerManager.instance != null)
        {
            player = PlayerManager.instance.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemy.moving);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.moving == false)
        {
            enemy.moving = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.moving == true)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.moveSpeed * Time.deltaTime);
            if (enemy.moving != false)
            {
                enemy.enemyAnimator.SetBool("isRunning", true);
            }
            else
            {
                enemy.enemyAnimator.SetBool("isRunning", false);
            }
            
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.moving == true)
        {
            enemy.moving = false;
            enemy.enemyAnimator.SetBool("isRunning", false);
        }
    }
}
