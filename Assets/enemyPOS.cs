using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPOS : MonoBehaviour
{
    public static enemyPOS instance;
    private EnemyMovement enemyM;
    private EnemyRange enemyR;
    public int enemyInSight;
    bool cek = false;
    private void Awake()
    {
        instance = this;
        
        enemyR = transform.parent.gameObject.GetComponent<EnemyRange>();
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
        if (enemyInSight <= 3)
        {
            enemyR.ContinueCS();
            Debug.Log("Enemy < 3");
        } else
        {
            enemyR.StopChase();
        }

        //if (enemyInSight > 3)
        //{
        //    enemyM.moving = false;
        //    enemyR.isTriggered = false;
        //    enemyM.enemyAnimator.SetBool("isRunning", false);
        //    enemyM.transform.position = new Vector2(enemyM.transform.position.x, enemyM.transform.position.y);
        //    Debug.Log("BISAAA");
        //    //FreeCheck();
        //    cek = true;
        //}
        //else if(cek)
        //{
        //    enemyR.isTriggered = true;
        //    enemyM.transform.position = Vector2.MoveTowards(enemyM.transform.position, enemyM.transform.position, enemyM.moveSpeed * Time.deltaTime);
        //    Debug.Log("Diam");
        //    cek = false;
        //}
    }
    

}
