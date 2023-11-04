using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
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
    public int attackDamage = 30;
    private Movement movement;
    private int count = 0;



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
        Debug.Log("COUNT is : " + count);
        if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("isJumping") == false )
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
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            rb.velocity = new Vector2(0, jumpHeight);
            
        }
    }
    private void FixedUpdate()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);
        if (Physics2D.Raycast(transform.position, down, 0.2f)) 
        {
            Debug.Log("check");
            isGround = true;

        }
        else
        {
            isGround = false;
        }
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
    void Attack()
    {
        animator.SetBool("isAttacking", true);
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

