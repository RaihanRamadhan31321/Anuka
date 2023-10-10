using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#define ZHAFRAN
public class Attack : MonoBehaviour
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
    private MovementAlt player;

    private void Start()
    {
        player = GetComponent<MovementAlt>();
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
                Invoke("MovementEnable", 0.5f);
            }

            else
            {
                Debug.Log("Spesial Attack Sedang Cooldown");
                //float remainingCooldown = specialCooldown - (Time.time - lastSpecialAttackTime);
                //Debug.Log("Spesial Attack Sedang Cooldown: " + remainingCooldown.ToString("F1") + " seconds");
                // Atur teks cooldown pada UI
                //cooldownUI.SetCooldownText(remainingCooldown);
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
            enemy.GetComponent<DarahMusuh>().TakeDamage(attackDamage);
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
            enemy.GetComponent<DarahMusuh>().SpecialTakeDamage(specialDamage);
            Debug.Log("Kena Spesial Attack Pukul");
        }
    }

    void MovementEnable()
    {
        player.OnEnableMovement();
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


}
