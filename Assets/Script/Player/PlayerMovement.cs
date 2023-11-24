using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    #region Variabel
    [HideInInspector] public bool isGround = false;
    [HideInInspector] public bool moving;
    [HideInInspector] public float atas;
    [HideInInspector] public float bawah;
    [HideInInspector] public GameObject child;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public GameObject landing;
    [HideInInspector] public GameObject playerGameObject;
    [HideInInspector] public Vector3 StartPoint;

    [HideInInspector] public PlayerHealthPoint playerHP;

    private float xvalue;
    private float yvalue;
    private TriggerBarrier trigger;
    [Header("JANGAN SENTUH!!!")]
    [Tooltip("Masukan animator game object ini ke sini")]
    public Animator animator;
    [SerializeField] private LayerMask layerMask;

    [Space(20f)]
    [Header("Player Movement Atribut")]
    public float jumpHeight = 8;
    public float speed = 8;
    [Tooltip("Ukuran BarrierOn untuk playerGameObject")]
    public float batasBarrier;
    #endregion
    void Start()
    {
        //langsung ambil komponen rb karena perlu 1 kali saja
        rb = GetComponent<Rigidbody2D>();
        OnEnableMovement();
        child = transform.Find("MoveLimit").gameObject;
        landing = FindAnyObjectByType<LandingPoint>().gameObject;
        landing.SetActive(false);

        playerGameObject = this.gameObject;
        //sementara
        playerHP = GetComponent<PlayerHealthPoint>();
        isGround = true;
        trigger = FindObjectOfType<TriggerBarrier>();

        StartPoint = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            

    }

    #region unitymethod
    void Update()
    {
        if (trigger.isFighting == true)
        {
            BarrierOn();

        }
        if (moving == true)
        {
            Move();
        }    
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            BarrierOff();
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
        }
    public void OnEnableMovement()
    {

        moving = true;
        animator.SetBool("isAttacking", false);
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
    public void Jump(float jumping)
    {
        rb.velocity = new Vector2(0, jumping);
    }
    public void BarrierOn()
    {
        float clampedValue = Mathf.Clamp(transform.position.x, atas,bawah);
        Vector3 pembatas = new Vector3(clampedValue, transform.position.y, transform.position.z);
        transform.position = pembatas;
    }
    public void BarrierOff()
    {
        Vector3 normal = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        trigger.isFighting = false;
        transform.position = normal;
    }

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

