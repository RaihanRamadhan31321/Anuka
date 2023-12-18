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
    public float basicATKCooldown;
    public float specialAttackKnockBack;
    //private float lastSpecialAttackTime = -1000.0f; // Inisialisasi dengan nilai yang memastikan serangan pertama bisa dilakukan
    private bool CanSpecialAttack = true;
    [SerializeField]private bool CanBasicAttack = true;
    private PlayerMovement player;
    private Animator animator;
    private GameObject enemy;
    [Tooltip("Untuk Mengatur seberapa jauh terlempar jika di pukul 3 kali")]
    [SerializeField] private float mundur;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float objHeight = 0.7f;
    Rigidbody2D rb;
    AudioManager audioManager;
    private int hitCount;
    PlayerHealthPoint healthPoint;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        //enemy = EnemyMovement.Instance.gameObject;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //basicattack
        if (Input.GetKeyDown(KeyCode.Mouse0) && player.isGround)
        {
            if (UIManager.instance.isPaused) return;
            if (CanBasicAttack)
            {
                StartCoroutine(BasicCooldown());
                Invoke("MovementEnable", 0.5f);
            }
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
        //if(enemy != null)
        //{
        //    if (transform.position.x > enemy.transform.position.x)
        //    {
        //        mundur = Mathf.Abs(mundur);
        //    }
        //    else
        //    {
        //        if (mundur > 0)
        //        {
        //            mundur = -mundur;
        //        }
        //    }
        //}
    }
    void BasicAttack()
    {
        //animasi
        audioManager.PlaySFX(audioManager.missHit);
        //deteksi musuh di radius jarak attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage ke musuh
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyMovement>().TakeDamage(attackDamage);
            enemy.GetComponentInChildren<Enemyattack>().GetHit();
            audioManager.PlaySFX(audioManager.hitBasicAtt);
        }
        CanBasicAttack = false;
    }

    void SpecialAttack()
    {
        //animasi
        player.animator.SetTrigger("IsHugeAttack");
        audioManager.PlaySFX(audioManager.missHugeHit);

        //deteksi musuh di radius jarak attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage ke musuh
        foreach (Collider2D enemy in hitEnemies)

        {
            enemy.GetComponent<EnemyMovement>().SpecialTakeDamage(specialDamage);
            enemy.transform.Find("AttackRange").GetComponent<Enemyattack>().GetHit();
            if(enemy.transform.position.x < player.transform.position.x)
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.left*specialAttackKnockBack, ForceMode2D.Impulse);
                //enemy.GetComponent<Rigidbody2D>().velocity = new Vector2( -specialAttackKnockBack, 0);
            }
            else
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.right * specialAttackKnockBack, ForceMode2D.Impulse);
                //enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Abs(specialAttackKnockBack), 0);
            }
            
            audioManager.PlaySFX(audioManager.hugeAtt);
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
    IEnumerator BasicCooldown()
    {
        player.OnDisableMovement();
        player.animator.SetTrigger("isAttacking");
        BasicAttack();
        yield return new WaitForSeconds(basicATKCooldown);
        CanBasicAttack = true;
    }
    public void GetHit()
    {
        player.OnDisableMovement();
        var effect = Instantiate(hitEffect, transform.position + Vector3.up * objHeight, Quaternion.identity);
        Destroy(effect, 0.2f);
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
