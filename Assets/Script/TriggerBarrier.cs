using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerBarrier : MonoBehaviour
{
    private PlayerMovement player;
    public GameObject companion;
    public BossManager boss;
    public bool isFighting = false;
    public bool waveStart = false;
    private bool isTriggered = false;
    
    [SerializeField] TriggerBarrier nextTriggerBarier;
    private int RandomNum;
    [SerializeField]private Transform EnemySpawnPoint1;
    [SerializeField]private Transform EnemySpawnPoint2;
    [SerializeField]private GameObject enemy;
    [SerializeField]private int enemySpawned = 1;
    [SerializeField] private int enemySpawnLimit;
    [SerializeField]private List<GameObject> characters;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameManager.Instance.onEnemyDeath += EnemyDeath;
    }
    private void OnDisable()
    {
        GameManager.Instance.onEnemyDeath -= EnemyDeath;
    }
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        if(StateManager.instance != null)
        {
            companion = StateManager.instance.gameObject;
        }
        boss = FindObjectOfType<BossManager>();
        
        if(boss != null)
        {
            boss.GetComponentInChildren<EnemyRange>().GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waveStart)
        {
            CharacterSorting();
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTriggered == false)
        {
            isFighting = true;
            
            player.atas = (transform.position.x) - player.batasBarrier;
            player.bawah = transform.position.x + player.batasBarrier;
            isTriggered = true;
            StartCoroutine(SpawnerEnemy());

        }
    }
    void CharacterSorting()
    {
        if(characters.Count >= 3)
        {
            characters.Sort(SortPos);
        }
        foreach (var character in characters)
        {
            character.GetComponent<SpriteRenderer>().sortingOrder = characters.IndexOf(character);
        }
        
    }
    private int SortPos(GameObject a, GameObject b)
    {
        if(a.gameObject.transform.position.y > b.gameObject.transform.position.y)
        {
            return -1;
        }else if(a.gameObject.transform.position.y < b.gameObject.transform.position.y)
        {
            return 1;
        }
        return 0;
    }
    private void SpawnEnemy()
    {
        
        RandomNum = UnityEngine.Random.Range(1, 3);
        GameObject enemy1 = null;
        switch (RandomNum)
        {
            case 1:
                enemy1 = Instantiate(enemy, EnemySpawnPoint1.position, EnemySpawnPoint1.rotation);

                enemySpawned++;
                enemies.Add(enemy1);
                break;
            case 2:
                enemy1 = Instantiate(enemy, EnemySpawnPoint2.position, EnemySpawnPoint2.rotation);
                enemySpawned++;
                enemies.Add(enemy1);
                break;
        }
        characters = new List<GameObject>(enemies);
        characters.Add(player.gameObject);
        characters.Add(boss.gameObject);
        if(companion != null)
        {
            characters.Add(companion.gameObject);
        }
        waveStart = true;
    }
    public void EndWave()
    {
        if(enemies.Count == 0)
        {
            Debug.Log("EnemyHabs");
            waveStart = false;
            if(SceneManager.GetActiveScene().buildIndex != 9)
            {
                waveStart = false;
                PlayerManager.instance.playerMV.BarrierOff();
                gameObject.SetActive(false);
                if (nextTriggerBarier == null)
                {
                    return;
                }
                player.trigger = nextTriggerBarier;
                player.currentWave++;
            }
            else
            {
                if (player.currentWave != 2)
                {
                    Debug.Log("StartWave2");
                    waveStart = true;
                    enemySpawnLimit = 7;
                    enemySpawned = 0;
                    boss.GetComponentInChildren<EnemyRange>().GetComponent<CircleCollider2D>().enabled = true;

                }
            }
            
        }
    }

    IEnumerator SpawnerEnemy()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1,4));
        
        if(enemySpawned <= enemySpawnLimit)
        {
            SpawnEnemy();
            StartCoroutine(SpawnerEnemy());
        }
    }
    public void EnemyDeath(GameObject enemy)
    {
        characters.Remove(enemy);
        enemies.Remove(enemy);
        EndWave();
    }
}
