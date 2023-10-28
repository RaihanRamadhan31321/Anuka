using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementAlt : MonoBehaviour
{
    //buat bisa gerak harus punya speed dulu dan xvalue
    public float speed = 8;
    //xvalue itu sebagai penyimpan nilai input
    private float xvalue;
    private float yvalue;
    public float delay = 3;
    float timer;
    public Animator animator;
    //akses rigidbody
    Rigidbody2D rb;
    public float jumpHeight = 8;
    public bool isGround = false;
    public bool moving;
    private bool isAttacking = false;
    private GameObject child;
    private GameObject landing;
    [SerializeField] private LayerMask layerMask;
    public int maxHealth = 100;
    int currentHealth;
    public float batasBarrier;
    private TriggerBarrier trigger;
    public float atas;
    public float bawah;


    // Start is called before the first frame update
    void Start()
    {
        //langsung ambil komponen rb karena perlu 1 kali saja
        rb = GetComponent<Rigidbody2D>();
        OnEnableMovement();
        child = transform.Find("MoveLimit").gameObject;
        landing = FindAnyObjectByType<LandingPoint>().gameObject;
        isGround = true;
        trigger = FindObjectOfType<TriggerBarrier>();

    }

    // Update is called once per frame
    #region unitymethod
    void Update()
    {
        if (trigger.isFighting == true)
        {
            Barrier();

        }
        //Debug.Log("COUNT is : " + count);
        if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("isJumping") == false)
        {
            OnDisableMovement();
            Attack();
            Invoke("OnEnableMovement", 0.5f);

        }
        if (moving == true)
        {
            Move();
        }
        //jika spasi ditekan maka tambah velocity ke sumbu y rigidbody     
        if (rb.velocity.y < 0 && isGround == true)
        {
            rb.gravityScale = 0;
            child.SetActive(true);
            rb.velocity = new Vector2(0, 0);
            landing.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            landing.SetActive(true);
            landing.transform.position = child.transform.position;
            rb.gravityScale = 1;
            Jump(jumpHeight);
            child.SetActive(false);

        }
    }

    private void FixedUpdate()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);
        RaycastHit2D groundHit = Physics2D.Raycast(child.transform.position, down, 0.5f, layerMask);

        Debug.DrawRay(child.transform.position, down * 0.5f, Color.red);

        if (groundHit.rigidbody != null) {
            isGround = true;
            animator.SetBool("isJumping", false);
    }
        else
        {
            isGround = false;
            animator.SetBool("isJumping", true);
        }
    }
    #endregion 
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

        //Debug.Log("IS MOVE");

    }
    public void Move()
    {
        //ambil berapa lama input horizontal 0 = no input,  1/-1 = hold
        xvalue = Input.GetAxis("Horizontal");
        yvalue = Input.GetAxis("Vertical");

        //ubah boolean isMoving jadi true
        if (xvalue == 0 && yvalue == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        //buat arah gerakan
        Vector2 arah = new Vector2(xvalue, yvalue).normalized;

        //kondisi jika berbalik
        if (xvalue < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (xvalue > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        //kalkulasi kecepatan arah
        this.transform.Translate(arah * Time.deltaTime * speed);
        }
    void Attack()
    {
        animator.SetBool("isAttacking", true);
    }
    void Jump(float jumping)
    {
        rb.velocity = new Vector2(0, jumping);
    }
    public void Barrier()
    {
        float clampedValue = Mathf.Clamp(transform.position.x, atas,bawah);
        Vector3 pembatas = new Vector3(clampedValue, transform.position.y, transform.position.z);
        transform.position = pembatas;
    }



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // if trigger sentuh tanah dengan tag "platform" allow jump

    //    if (collision.tag == "Platform" )
    //    {
    //        count++;
    //        isGround = true;
    //        animator.SetBool("isJumping", false);
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Platform" ) {
    //        count--;
    //        isGround = false;
    //        animator.SetBool("isJumping", true);
    //    }
    //}
    ////private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (count == 2)
    //    {
    //        transform.Find("HitRange").gameObject.SetActive(false);
    //    } else if (count == 0)
    //    {
    //        transform.Find("HitRange").gameObject.SetActive(true);
    //    }
    //}
}




/*
 * PRECIOUS
 * panggil child dari object terus set non aktif
 *          cari child              set ke aktif
 * |--------V---------------| |---------V---------------|
 * transform.Find("HitRange").gameObject.SetActive(true);
 * 
 * SceneManager.LoadScene("nama");
 
 */

