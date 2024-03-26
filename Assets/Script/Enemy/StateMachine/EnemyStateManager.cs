using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public static EnemyStateManager instance;
    public Animator animator;
    public Rigidbody2D rb;
    public EnemyManager enemyManager ;
    public GameObject enemy;
    public GameObject player;
    public PlayerManager playerManager ;

    public EnemyBaseState currentState;
    public EnemyIdle s_Idle = new EnemyIdle();
    public EnemyChase s_Chase = new EnemyChase();
    public EnemyDeath s_Death = new EnemyDeath();
    public EnemyAtk s_Attack = new EnemyAtk();
    public EnemyGetHit s_GetHit = new EnemyGetHit();

    public bool isWaiting = false;
    public float waitingTime;
    public Vector3 jarak = new Vector3(0.5f, 0, 0);
    public bool isDead;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        enemyManager = EnemyManager.instance.GetComponent<EnemyManager>();
        enemy = EnemyManager.instance.gameObject;
        playerManager = PlayerManager.instance.GetComponent<PlayerManager>();
        player = PlayerManager.instance.gameObject;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentState = s_Idle;
        if (s_Idle != null)
            s_Idle.StartState(this);
        else
            Debug.LogError("idleState is null");
        currentState.StartState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.StartState(this);
        Debug.Log("Change State To : " + state);

    }

    public void StartWaitingCounter(float time)
    {
        StartCoroutine(WaitingCounter(time));
    }

    IEnumerator WaitingCounter (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}
