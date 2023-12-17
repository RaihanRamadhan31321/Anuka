using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompMovement : MonoBehaviour
{
    public static CompMovement Instance;
    public Animator compAnimator;
    public float moveSpeed;
    [SerializeField] private GameObject player;
    public bool moving;
    public int maxHealth = 100;
    private Collider2D colider;
    public int currentHealth;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public bool isDead = false;
    public GameObject target;

    private void OnEnable()
    {
        GameManager.Instance.onPlayerSpawn.AddListener(Initialized);
    }
    private void OnDisable()
    {
        GameManager.Instance.onPlayerSpawn.RemoveAllListeners();
    }
    private void Initialized()
    {
        player = PlayerManager.instance.gameObject;

    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (player == null)
        {
            player = PlayerManager.instance.gameObject;
        }
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<Collider2D>();
        compAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Rotate();

    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;


            //animasi kena pukul

            if (currentHealth <= 0)
            {
                Invoke("Die", 0.5f);
            }

        }
        else
        {
            return;
        }
    }
    public void SpecialTakeDamage(int specialDamage)
    {
        if (!isDead)
        {
            currentHealth -= specialDamage;

            //animasi kena pukul

            if (currentHealth <= 0)
            {
                Invoke("Die", 0.5f);
            }
        }
        else { return; }

    }

    void Die()
    {
        GameManager.Instance.onEnemyDeath?.Invoke(this.gameObject);
        compAnimator.SetTrigger("isDead");
        colider.enabled = false;
        isDead = true;

        //animasi mati

        //disable musuh
    }
    public void DeleteThis()
    {
        Destroy(gameObject, 5f);
    }


    void Rotate()
    {
        if (!isDead)
        {
            if (target != null)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 0);
                }
                if (transform.position.x > target.transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 0);
                }
            }
        }

    }
}
