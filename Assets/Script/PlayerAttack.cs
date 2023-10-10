using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 30;
    private bool isAttacking = false;
    private Movement movement;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }

    }

    void Attack()
    {
        //animasi
        animator.SetTrigger("Attack");

        //deteksi musuh di radius jarak attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage ke musuh
        //foreach (Collider2D enemy in hitEnemies)
       // {
       //     enemy.GetComponent<DarahMusuh>().TakeDamage(attackDamage);
       // }

        // Set karakter selesai menyerang
        animator.SetBool("isAttacking", true);

        //delay antara memukul dan lari 
        StartCoroutine(EnableMovementAfterDelay(0.5f));

    }

    //codingan buat delay
    IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Aktifkan kembali input gerakan
        movement = GetComponent<Movement>();
        if (movement != null)
        {
            movement.enabled = true;
            Debug.Log("Movement Enable");
        }
        // Set karakter selesai menyerang
        animator.SetBool("isAttacking", false);
    }

    //Buat custom attackpoint
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);   
    }
}
