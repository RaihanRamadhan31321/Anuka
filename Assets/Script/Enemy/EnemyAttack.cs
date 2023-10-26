using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyattack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public int attackDamage;
    public int specialDamage;
    public float specialCooldown = 3.0f; // Waktu cooldown untuk serangan khusus
    //private float lastSpecialAttackTime = -1000.0f; // Inisialisasi dengan nilai yang memastikan serangan pertama bisa dilakukan
    private bool CanSpecialAttack = true;
    private EnemyMovement enemy;
    private DarahPlayer player;
    private Rigidbody2D rb;

    private void Start()
    {
        enemy = transform.parent.gameObject.GetComponent<EnemyMovement>();
        player = FindObjectOfType<DarahPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.AddForce(Vector2.zero);
    }
    void BasicAttack()
    {
        //animasi
        enemy.enemyAnimator.SetBool("isAttacking", true);

        //deteksi musuh di radius jarak attack
        //Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        ////damage ke musuh
        //foreach (Collider2D player in hitPlayer)
        //{
        enemy.OnDisableMovement();
        player.TakeDamage(attackDamage);
        Debug.Log("Kena Player");
        Invoke("MovementEnable", 0.7f);
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.enemyAnimator.GetBool("isAttacking") == false)
        {
            Debug.Log("stay");
            BasicAttack();
            enemy.moving = false;
        }
    }
    /*void SpecialAttack()
    {
        //animasi
        player.animator.SetTrigger("IsHugeAttack");

        //deteksi musuh di radius jarak attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage ke musuh
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyMovement>().SpecialTakeDamage(specialDamage);
            Debug.Log("Kena Spesial Attack Pukul");
        }
    }*/

    void MovementEnable()
    {
        enemy.OnEnableMovement();
    }

    IEnumerator SpecialAttackCooldown()
    {
        yield return new WaitForSeconds(specialCooldown);
        CanSpecialAttack = true;
    }

    //display
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    //void OnTriggerStay2D(Collider2D collision)
    //{

    //    if (collision.CompareTag("Player"))
    //    {
            

    //    }
    //}
}
