using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public BossState currentState;
    public EnemyMovement bossMV;
    public Enemyattack bossAT;
    public EnemyRange bossRG;
    public Animator animator;
    public LineRenderer lineRenderer;
    public Rigidbody2D rb;
    public SpriteRenderer rbSprite;
    public GameObject player;
    public PlayerManager playerManager;
    public StateManager compManager;

    public Vector3 jarak;
    public bool cd = false;
    public bool isCharging = false;
    public Vector3 chargeTarget;
    public float knockBack;

    public BossIdle idleState = new BossIdle();
    public BossChase chaseState = new BossChase();
    public BossAttack attackState = new BossAttack();
    public BossRun runState = new BossRun();
    public BosDeath deathState = new BosDeath();
    public BossSpecialAttack specialAttackState = new BossSpecialAttack();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        rb = GetComponent<Rigidbody2D>();

        rbSprite = GetComponent<SpriteRenderer>();
        bossMV = GetComponent<EnemyMovement>();
        bossAT = GetComponentInChildren<Enemyattack>();
        player = PlayerManager.instance.gameObject;
        compManager = StateManager.instance;
        playerManager = PlayerManager.instance;
        bossRG = GetComponentInChildren<EnemyRange>();

        currentState = idleState;
        if (idleState != null)
            idleState.StartState(this);
        else
            Debug.LogError("idleState is null");
        currentState.StartState(this);
    }
    private void Update()
    {
        
        
        if (bossRG.enemyFlw != null)
        {
            if (bossMV.transform.position.x >= bossRG.enemyFlw.transform.position.x)
            {
                jarak = new Vector3(2, 0, 0);
            }
            else
            {
                jarak = new Vector3(-2, 0, 0);
            }
        }
        currentState.UpdateState(this);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
    public void SwitchState(BossState state)
    {
        currentState = state;
        state.StartState(this);
        Debug.Log("Change State To : " + state);

    }
    public void WaitBeforeCharge()
    {
        StartCoroutine(ChargeCD());
    }
    public void SpecialAttackOnCD()
    {
        StartCoroutine(SpecialAttackCD());
    }
    public void SwitchTarget()
    {
        StartCoroutine(SwitchTargetCD());
    }
    private IEnumerator ChargeCD()
    {
        yield return new WaitForSeconds(4);
        
        chargeTarget = bossRG.enemyFlw.transform.position;
        isCharging = true;
        specialAttackState.isWaiting = false;
    }
    private IEnumerator SpecialAttackCD()
    {
        yield return new WaitForSeconds(10);
        cd = false;
    }
    private IEnumerator SwitchTargetCD()
    {
        yield return new WaitForSeconds(8);
        if(bossRG.enemyFlw == player)
        {
            bossRG.enemyFlw = StateManager.instance.compMovement.GetComponent<CompMovement>().gameObject;
        }
        else
        {
            bossRG.enemyFlw = player;
        }
        
    }
}
