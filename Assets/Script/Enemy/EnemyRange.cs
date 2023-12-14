using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public static EnemyRange instance;
    private float batasAtasY = -0.13f;
    private float batasBawahY = -9.37f;
    public bool isTriggered = false;
    public EnemyMovement enemy;
    public enemyPOS POS;
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        enemy = transform.parent.gameObject.GetComponent<EnemyMovement>();
        POS = GetComponentInChildren<enemyPOS>();
        if (PlayerManager.instance != null)
        {
            player = PlayerManager.instance.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (!enemy.moving)
            {
                enemy.moving = true;
            }
            else
            {
                enemy.moving = false;
            }
            
        }
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
            POS.CheckSite();
            //RaycastHit2D raycastHit2D = Physics2D.Raycast(enemy.transform.position, player.transform.position, Vector3.Distance(enemy.transform.position, player.transform.position));
            //if (raycastHit2D.collider.CompareTag("Enemy"))
            //{
            //    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, raycastHit2D.point, enemy.moveSpeed * Time.deltaTime);
            //}
        }
        else
        {
            if (enemy.enemyAnimator.GetBool("isRunning") == false)
            {
                enemy.enemyAnimator.SetBool("isRunning", true);
            }
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.moveSpeed * Time.deltaTime);
            isTriggered = true;
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
    public IEnumerator ContinueChase()
    {
        StopChase();
        yield return new WaitForSeconds(0.01f);
        enemy.enemyAnimator.SetBool("isRunning", true);
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.moveSpeed * Time.deltaTime);
        Debug.Log("CONTINUE");
    }
    public void ContinueCS()
    {
        enemy.enemyAnimator.SetBool("isRunning", true);
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.moveSpeed * Time.deltaTime);
    }
    public void StopChase()
    {
        enemy.enemyAnimator.SetBool("isRunning", false);
        enemy.transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        Debug.Log("STOP");
    }


}
