using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemyattack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public int attackDamage;
    public int specialDamage;
    public float attackCooldown = 3.0f; // Waktu cooldown untuk serangan khusus
    //private float lastSpecialAttackTime = -1000.0f; // Inisialisasi dengan nilai yang memastikan serangan pertama bisa dilakukan
    private bool CanAttack = true;
    private EnemyMovement enemy;
    private PlayerHealthPoint playerHP;
    private PlayerAttack playerAtk;
    private Rigidbody2D rb;
    private bool cd = true;
    private int hitCount;
    private Vector3 stay;
    [Tooltip("Untuk Mengatur seberapa jauh terlempar jika di pukul 3 kali")]
    [SerializeField] private float mundur;

    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float objHeight = 0.7f;
    [SerializeField] private Color32 warna;
    private SpriteRenderer sr;
    [SerializeField] private CinemachineImpulseSource camShake;

    private void Start()
    {
        enemy = transform.parent.gameObject.GetComponent<EnemyMovement>();
        sr = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        playerHP = FindObjectOfType<PlayerHealthPoint>();
        playerAtk = FindObjectOfType<PlayerAttack>();
        rb = GetComponent<Rigidbody2D>();
        camShake = GetComponent<CinemachineImpulseSource>();   
        animator = enemy.enemyAnimator;
        stay = new Vector3(1.51f, 0.26f, 0);
    }
    private void Update()
    {
        transform.localPosition = stay;
        rb.AddForce(Vector2.zero);
        if (transform.position.x > playerHP.transform.position.x)
        {
            mundur = Mathf.Abs(mundur);
        }
        else
        {
            if (mundur > 0)
            {
                mundur = -mundur;
            }
        }
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
            enemy.OnDisableMovement();
            Debug.Log("CHECK");
        }
    }
    void MovementEnable()
    {
        enemy.OnEnableMovement();
        enemy.rb.velocity = new Vector2(0,0);
        animator.SetBool("getHit", false);
    }

    IEnumerator AttackCooldown()
    {
        Debug.Log("CD");
        enemy.OnDisableMovement();
        yield return new WaitForSeconds(attackCooldown);

        Debug.Log("CD2");
        enemy.OnEnableMovement();
        CanAttack = true;
        cd = false;
    }
    public void GetHit()
    {
        IEnumerator HitCooldown()
        {
            sr.color = warna;
            yield return new WaitForSeconds(0.2f);
            sr.color = Color.white;
        }
        enemy.OnDisableMovement();
        camShake.GenerateImpulse(1);
        var effect = Instantiate(hitEffect, transform.position + Vector3.up * objHeight, Quaternion.identity);
        Destroy(effect, 0.2f);
        StartCoroutine(HitCooldown());

        animator.SetBool("getHit", true);
        if (hitCount == 3)
        {
            
            Vector2 back = new Vector2(mundur, 0);
            enemy.rb.velocity = back;

            hitCount = 0;
        }
        Invoke("MovementEnable", 0.4f);
        hitCount++;

    }

    //display
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
