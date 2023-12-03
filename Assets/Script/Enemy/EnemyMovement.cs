using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator enemyAnimator; 
    public float moveSpeed;
    private GameObject player;
    public bool moving;
    public int maxHealth = 100;
    int currentHealth;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public bool isDead = false;

    private void OnEnable()
    {
        GameManager.Instance.onPlayerSpawn.AddListener(Initialized);
    }
    private void OnDisable()
    {
        GameManager.Instance.onPlayerSpawn.RemoveAllListeners();
    }
    private void Initialized()
    {
        player = PlayerManager.instance.gameObject;
    }
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        Rotate();
    
    }

    public void TakeDamage(int damage)
    {
        if(!isDead)
        {
            currentHealth -= damage;


            //animasi kena pukul

            if (currentHealth < 0)
            {
                   Die();
            }

        } else
        {
            return;
        }
    }
    public void SpecialTakeDamage(int specialDamage)
    {
        if (!isDead)
        {
            currentHealth -= specialDamage;

            //animasi kena pukul

            if (currentHealth < 0)
            {
                Die();
            }
        }else { return; }
        
    }

    void Die()
    {
        Debug.Log("MATI DIA");
        enemyAnimator.SetTrigger("isDead");
        isDead = true;

        //animasi mati

        //disable musuh
    }
    public void DeleteThis()
    {
        Destroy(gameObject, 5f);
    }


    void Rotate()
    {
        if(!isDead)
        {
            if (player != null)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 0);
                }
                if (transform.position.x > player.transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 0);
                }
            }
        }     
        
    }
    public void OnDisableMovement()
    {
        moving = false;
        enemyAnimator.SetBool("isRunning", false);
        //Debug.Log("IS NOT MOVE");
    }
    public void OnEnableMovement()
    {

        moving = true;
        enemyAnimator.SetBool("isAttacking", false);

        //Debug.Log("IS MOVE");

    }
}
