using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CompAttack : MonoBehaviour
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
    private CompMovement companion;
    private PlayerManager player;
    private Rigidbody2D rb;
    private bool cd = true;
    private int hitCount;
    public Collider2D colider;
    public bool enemyInRange=false;
    private CompVision companionVision;

    private Vector3 stay;
    [Tooltip("Untuk Mengatur seberapa jauh terlempar jika di pukul 3 kali")]
    [SerializeField] private float mundur;

    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float objHeight = 0.7f;
    [SerializeField] private Color32 warna;
    private SpriteRenderer sr;
    [SerializeField] private CinemachineImpulseSource camShake;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        companion = transform.parent.gameObject.GetComponent<CompMovement>();
        companionVision = companion.GetComponent<CompVision>();
        sr = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        camShake = GetComponent<CinemachineImpulseSource>();
        animator = companion.compAnimator;
        colider = GetComponent<Collider2D>();
        player = PlayerManager.instance.GetComponent<PlayerManager>();
        stay = new Vector3(1.51f, 0.26f, 0);
    }
    private void Update()
    {
        transform.localPosition = stay;
        rb.AddForce(Vector2.zero);
        if (companion.transform.position.x > player.playerHP.transform.position.x)
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
    public void GetHit()
    {
        if (companion.isDead == false)
        {
            IEnumerator HitCooldown()
            {
                sr.color = warna;
                yield return new WaitForSeconds(0.2f);
                sr.color = Color.white;
            }
            camShake.GenerateImpulse(1);
            var effect = Instantiate(hitEffect, transform.position + Vector3.up * objHeight, Quaternion.identity);
            Destroy(effect, 0.2f);
            StartCoroutine(HitCooldown());

            animator.SetBool("getHit", true);
            if (hitCount == 3)
            {

                Vector2 back = new Vector2(mundur, 0);
                companion.rb.velocity = back;

                hitCount = 0;
            }
            Invoke("MovementEnable", 0.4f);
            hitCount++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyInRange = false;
        }
    }

    //display
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
