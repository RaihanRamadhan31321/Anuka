using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#define ZHAFRAN
public class Enemyattack : MonoBehaviour
{
#if ZHAFRAN
    public Animator animator;
#endif
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public int attackDamage;
    public int specialDamage;
    public float attackCooldown = 3.0f; // Waktu cooldown untuk serangan khusus
    //private float lastSpecialAttackTime = -1000.0f; // Inisialisasi dengan nilai yang memastikan serangan pertama bisa dilakukan
    private bool CanAttack = true;
    private EnemyMovement enemy;
    private DarahPlayer playerHP;
    private Attack playerAtk;
    private Rigidbody2D rb;
    private bool cd = true;

    private void Start()
    {
        enemy = transform.parent.gameObject.GetComponent<EnemyMovement>();
        playerHP = FindObjectOfType<DarahPlayer>();
        playerAtk = FindObjectOfType<Attack>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.AddForce(Vector2.zero);
        Debug.Log(CanAttack);
        
    }
    void BasicAttack()
    {
        Debug.Log("BASIC");
        enemy.enemyAnimator.SetBool("isAttacking", true);
        enemy.OnDisableMovement();
        playerHP.TakeDamage(attackDamage);
        playerAtk.GetHit();
        CanAttack = false;
        cd = true;
        Invoke("MovementEnable", 0.7f);
        
    }
    void CooldownBasicAttack ()
    {
        cd = false;
        StartCoroutine(AttackCooldown());
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (cd)
            {
                CooldownBasicAttack();
            }
            if (CanAttack)
            {
                BasicAttack();
            }
            enemy.moving = false;
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player") && enemy.enemyAnimator.GetBool("isAttacking") == false)
    //    {
    //        if (CanAttack)
    //        {
    //            BasicAttack();
    //            CanAttack = false;
    //        }
    //        else
    //        {
    //            StartCoroutine(AttackCooldown());
    //        }
    //        enemy.moving = false;
    //    }
    //}
    /*void SpecialAttack()
    {
        //animasi
        playerHP.animator.SetTrigger("IsHugeAttack");

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

    IEnumerator AttackCooldown()
    {
        Debug.Log("CD");
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
        cd = false;
    }

    //display
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
