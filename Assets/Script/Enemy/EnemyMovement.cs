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
    void Start()
    {
        currentHealth = maxHealth;
        player = FindObjectOfType<MovementAlt>().gameObject;
    }

    void Update() 
    {
        Rotate();
    
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //animasi kena pukul

        if (currentHealth < 0)
        {
               Die();
        }
    }
    public void SpecialTakeDamage(int specialDamage)
    {
        currentHealth -= specialDamage;

        //animasi kena pukul

        if (currentHealth < 0)
        {
             Die();
        }
    }

    void Die()
    {
        Debug.Log("MATI DIA");

        //animasi mati

        //disable musuh
    }


    void Rotate()
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
