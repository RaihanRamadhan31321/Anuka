using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CompVision : MonoBehaviour
{
    public static CompVision instance;
    private float batasAtasY = -0.13f;
    private float batasBawahY = -9.37f;
    public List<GameObject> enemies = new List<GameObject>();
    public bool isTriggered = false;
    public Collider2D colider;
    public CompMovement companion;
    public GameObject closestEnemy;
    public float closestEnemyDistance = Mathf.Infinity;

    public enemyPOS POS;
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        colider = GetComponent<Collider2D>();
        companion = transform.parent.gameObject.GetComponent<CompMovement>();
        POS = GetComponentInChildren<enemyPOS>();
        if (PlayerManager.instance != null)
        {
            player = PlayerManager.instance.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, this.transform.position);
            if(distance < closestEnemyDistance)
            {
                closestEnemy = enemy;
                closestEnemyDistance = distance;
            }
        }
        if(enemies.Count == 1)
        {
            closestEnemy = enemies[0];
        }
        //Debug.Log(enemy.moving);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.gameObject); 
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && companion.moving == true && companion.isDead == false)
        {
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Remove(collision.gameObject);
        }
    }
    private void MovementTactic()
    {
        if ((Vector3.Distance(companion.transform.position, player.transform.position)) <= 15) //mid distance
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
            if (companion.compAnimator.GetBool("isRunning") == false)
            {
                companion.compAnimator.SetBool("isRunning", true);
            }
            companion.transform.position = Vector2.MoveTowards(companion.transform.position, player.transform.position, companion.moveSpeed * Time.deltaTime);
            isTriggered = true;
        }
    }

    //private void FreeCheck()
    //{
    //    //UnityEngine.Random.Range(enemy.transform.position.y, enemy.transform.position.y + 0.1f)
    //    Vector2 atas = new Vector2(enemy.transform.position.x, companion.transform.position.y - 0.02f);
    //    Vector2 bawah = new Vector2(companion.transform.position.x, enemy.transform.position.y + 0.02f);
    //    if (companion.transform.position.y - batasAtasY < -4.805f)
    //    {
    //        //gerak ke bawah
    //        enemy.transform.position = Vector2.MoveTowards(companion.transform.position, bawah, enemy.moveSpeed * Time.deltaTime);
    //        Debug.Log("GO TO : " + bawah);
    //    }
    //    else
    //    {
    //        //gerak ke atas

    //        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, atas, companion.moveSpeed * Time.deltaTime);
    //    }
    //}
    //public IEnumerator ContinueChase()
    //{
    //    StopChase();
    //    yield return new WaitForSeconds(0.01f);
    //    enemy.enemyAnimator.SetBool("isRunning", true);
    //    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, companion.moveSpeed * Time.deltaTime);
    //    Debug.Log("CONTINUE");
    //}
    //public void ContinueCS()
    //{
    //    enemy.enemyAnimator.SetBool("isRunning", true);
    //    companion.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.moveSpeed * Time.deltaTime);
    //}
    //public void StopChase()
    //{
    //    enemy.enemyAnimator.SetBool("isRunning", false);
    //    enemy.transform.position = new Vector2(companion.transform.position.x, enemy.transform.position.y);
    //    Debug.Log("STOP");
    //}


}
