using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarahMusuh : MonoBehaviour
{
    public Animator enemyAnimator; // Reference to the Animator component of your enemy
    private bool playerInside = false;
    [SerializeField] private float moveSpeed;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& moving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            playerInside = true;
            enemyAnimator.SetBool("isRunning", true);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Change "Player" to the actual tag of your player object
        {
            playerInside = true;
            enemyAnimator.SetBool("isRunning", true);
        }
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            enemyAnimator.SetBool("isRunning", false);
        }
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
        //Debug.Log("IS NOT MOVE");
    }
    public void OnEnableMovement()
    {

        moving = true;
        enemyAnimator.SetBool("isAttacking", false);

        //Debug.Log("IS MOVE");

    }
}
