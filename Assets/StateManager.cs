using System.Collections;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public CompMovement compMovement;
    public CompVision compVision;
    public CompAttack compAttack;
    public GameObject player;
    public PlayerManager playerManager;
    public Vector3 jarakPlayer;
    public bool cd;
    public bool isInAttackRange;
    public bool isDead = false;

    public State currentState;
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer rbSprite;
    public Follow followState = new Follow();
    public Idle idleState = new Idle();
    public Chase chaseState = new Chase();
    public Attack attackState = new Attack();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        compMovement = GetComponent<CompMovement>();
        compVision = GetComponentInChildren<CompVision>();
        compAttack = GetComponentInChildren<CompAttack>();
        player = PlayerManager.instance.gameObject;
        playerManager = PlayerManager.instance.GetComponent<PlayerManager>();

        
        currentState = idleState;
        if (idleState != null)
            idleState.StartState(this);
        else
            Debug.LogError("idleState is null");
        currentState.StartState(this);
    }
    private void Update()
    {
        if (compMovement.transform.position.x >= player.transform.position.x)
        {
            jarakPlayer = new Vector3(2, 0, 0);
        }
        else
        {
            jarakPlayer = new Vector3(-2, 0, 0);
        }
        currentState.UpdateState(this);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
    public void SwitchState(State state)
    {
        currentState = state;
        state.StartState(this);
        Debug.Log("Change State To : " + state);

    }
    public void AttackCooldown()
    {
        StartCoroutine(AttackCD());
    }
    private IEnumerator AttackCD()
    {

        yield return new WaitForSeconds(1f);
        cd = false;
    }
}
