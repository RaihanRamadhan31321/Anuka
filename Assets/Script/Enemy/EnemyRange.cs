using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public static EnemyRange instance;
    private float batasAtasY = -0.13f;
    private float batasBawahY = -9.37f;
    public EnemyMovement enemy;
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
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
        if (collision.CompareTag("Player") && enemy.moving == true && enemy.isDead == false)
        {
            MovementTactic();
            if (enemy.moving != false)
            {
                enemy.enemyAnimator.SetBool("isRunning", true);
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
    private void MovementTactic()
    {
        if ((Vector3.Distance(enemy.transform.position, player.transform.position)) <= 15) //mid distance
        {
            enemyPOS.instance.CheckSite();
            //RaycastHit2D raycastHit2D = Physics2D.Raycast(enemy.transform.position, player.transform.position, Vector3.Distance(enemy.transform.position, player.transform.position));
            //if (raycastHit2D.collider.CompareTag("Enemy"))
            //{
            //    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, raycastHit2D.point, enemy.moveSpeed * Time.deltaTime);
            //}
        }
        else
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.moveSpeed * Time.deltaTime);
        }
    }

    private void FreeCheck()
    {
        //UnityEngine.Random.Range(enemy.transform.position.y, enemy.transform.position.y + 0.1f)
        Vector2 atas = new Vector2(enemy.transform.position.x, enemy.transform.position.y - 0.02f);
        Vector2 bawah = new Vector2(enemy.transform.position.x, enemy.transform.position.y + 0.02f);
        if(enemy.transform.position.y - batasAtasY < -4.805f)
        {
            //gerak ke bawah
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, bawah, enemy.moveSpeed * Time.deltaTime);
            Debug.Log("GO TO : "+ bawah);
        }
        else
        {
            //gerak ke atas
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, atas, enemy.moveSpeed * Time.deltaTime);
        }
    }


}
