using UnityEngine;


public class Controller : MonoBehaviour
{
    //buat bisa gerak harus punya speed dulu dan xvalue
    public float speed = 8;
    //xvalue itu sebagai penyimpan nilai input
    private float xvalue;
    public Animator animator;
    //akses rigidbody
    Rigidbody2D rb;
    public float jumpHeight = 5;
    bool isGround = false;
    bool moving;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage;
    public int specialDamage;

    public float specialCooldown = 3.0f; // Waktu cooldown untuk serangan khusus
    private float lastSpecialAttackTime = -1000.0f; // Inisialisasi dengan nilai yang memastikan serangan pertama bisa dilakukan
    //public CooldownSpecial cooldownUI;




    private Controller movement;



    // Start is called before the first frame update
    void Start()
    {
        //langsung ambil komponen rb karena perlu 1 kali saja
        rb = GetComponent<Rigidbody2D>();
        OnEnableMovement();
    }

    // Update is called once per frame
    void Update()
    {
        //Klik kiri
        if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("isJumping") == false )
        { 
            OnDisableMovement();
            Attack();
            Invoke("OnEnableMovement", 0.5f);
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && animator.GetBool("isJumping") == false)
        {
            if (CanSpecialAttack()) // Periksa apakah serangan khusus dapat dilakukan
            {
                OnDisableMovement();
                SpecialAttack();
                lastSpecialAttackTime = Time.time; // Set waktu serangan khusus terakhir
                Invoke("OnEnableMovement", 0.5f);
            }

            else
            {
                float remainingCooldown = specialCooldown - (Time.time - lastSpecialAttackTime);
                Debug.Log("Spesial PlayerAttack Sedang Cooldown: " + remainingCooldown.ToString("F1") + " seconds");
                // Atur teks cooldown pada UI
                //cooldownUI.SetCooldownText(remainingCooldown);
            }
        }
        if (moving == true)
        {
            Move();
        }
        //jika spasi ditekan maka tambah velocity ke sumbu y rigidbody
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            rb.velocity = new Vector2(0, jumpHeight);
            
        }
    }
    void Attack()
    {
        //animasi
        animator.SetBool("isAttacking", true);

        //deteksi musuh di radius jarak attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage ke musuh
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyMovement>().TakeDamage(attackDamage);
            Debug.Log("Kena Area Pukul");
        }
    }

    void SpecialAttack()
    {
        //animasi
        animator.SetBool("isSpecialAttacking", true);

        //deteksi musuh di radius jarak attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage ke musuh
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyMovement>().SpecialTakeDamage(specialDamage);
            Debug.Log("Kena Spesial PlayerAttack Pukul");
        }
    }

    bool CanSpecialAttack()
    {
        // Periksa apakah sudah cukup waktu berlalu sejak serangan khusus terakhir
        return Time.time - lastSpecialAttackTime >= specialCooldown;
    }

    //display
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void OnDisableMovement()
    {
        moving = false;
        animator.SetBool("isMoving", false);
        //Debug.Log("IS NOT MOVE");
    }
    public void OnEnableMovement()
    {
        moving = true;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isSpecialAttacking", false);
        //Debug.Log("IS MOVE");
    }
    void Move()
    {
        //ambil berapa lama input horizontal 0 = no input,  1/-1 = hold
        xvalue = Input.GetAxis("Horizontal");

        //ubah boolean isMoving jadi true
        if (xvalue != 0)
        {
            animator.SetBool("isMoving", true);
        }else
        {
            animator.SetBool("isMoving", false);
        }

        //buat arah gerakan
        Vector2 arah = new Vector2(xvalue, 0);

        //kondisi jika berbalik
        if (xvalue < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(xvalue > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //kalkulasi kecepatan arah
        transform.Translate(arah * Time.deltaTime * speed);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if trigger sentuh tanah dengan tag "platform" allow jump
        if (collision.tag == "Platform" )
        {
            isGround = true;
            animator.SetBool("isJumping", false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform" ) { 
            isGround = false;
            animator.SetBool("isJumping", true);
        }
    }
}
