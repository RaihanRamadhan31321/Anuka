using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#define ZHAFRAN
public class PlayerAttack : MonoBehaviour
{
#if ZHAFRAN
    public Animator animator;
#endif
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage;
    public int specialDamage;
    public float specialCooldown = 3.0f; // Waktu cooldown untuk serangan khusus
    //private float lastSpecialAttackTime = -1000.0f; // Inisialisasi dengan nilai yang memastikan serangan pertama bisa dilakukan
    private bool CanSpecialAttack = true;
    private PlayerMovement player;
    private Animator animator;
    private GameObject enemy;
    [Tooltip("Untuk Mengatur seberapa jauh terlempar jika di pukul 3 kali")]
    [SerializeField] private float mundur;
    Rigidbody2D rb;
    private int hitCount;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        enemy = FindObjectOfType<EnemyMovement>().gameObject;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //basicattack
        if (Input.GetKeyDown(KeyCode.Mouse0) && player.animator.GetBool("isJumping") == false)
        {
            player.OnDisableMovement();
            BasicAttack();
            Invoke("MovementEnable", 0.5f);

        }
        //specialattack
        if (Input.GetKeyDown(KeyCode.Mouse1) && player.isGround)
        {
            if (CanSpecialAttack) // Periksa apakah serangan khusus dapat dilakukan
            {
                player.OnDisableMovement();
                SpecialAttack();
                CanSpecialAttack = false;
                StartCoroutine(SpecialAttackCooldown());
                //lastSpecialAttackTime = Time.time; // Set waktu serangan khusus terakhir
                Invoke("MovementEnable", 1f);
            }

            else
            {
                Debug.Log("Spesial PlayerAttack Sedang Cooldown");
            }
        }

        if (transform.position.x > enemy.transform.position.x)
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
        //animasi
        player.animator.SetBool("isAttacking", true);

        //deteksi musuh di radius jarak attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage ke musuh
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyMovement>().TakeDamage(attackDamage);
            enemy.transform.Find("AttackRange").GetComponent<Enemyattack>().GetHit();
            Debug.Log("Kena Area Pukul");
        }
    }

    void SpecialAttack()
    {
        //animasi
        player.animator.SetTrigger("IsHugeAttack");

        //deteksi musuh di radius jarak attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage ke musuh
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyMovement>().SpecialTakeDamage(specialDamage);
            Debug.Log("Kena Spesial PlayerAttack Pukul");
        }
    }

    void MovementEnable()
    {
        player.OnEnableMovement();
        animator.SetBool("getHit", false);
        rb.velocity = new Vector2(0,0);
    }

    IEnumerator SpecialAttackCooldown()
    {
        yield return new WaitForSeconds(specialCooldown);
        CanSpecialAttack = true;
    }
    public void GetHit()
    {
        player.OnDisableMovement();
        animator.SetBool("getHit", true);
        if(hitCount == 3) 
        { 
            Vector2 back = new Vector2(mundur, 0);
            rb.velocity = back;
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
