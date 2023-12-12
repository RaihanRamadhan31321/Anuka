using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBarrier : MonoBehaviour
{
    private PlayerMovement player;
    public bool isFighting = false;
    public bool waveStart = false;
    private bool isTriggered = false;
    private int RandomNum;
    [SerializeField]private Transform EnemySpawnPoint1;
    [SerializeField]private Transform EnemySpawnPoint2;
    [SerializeField]private GameObject enemy;
    [SerializeField]private int enemySpawned = 1;
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
            EnemySpawnPoint1.position = new Vector3(player.atas - 2, transform.position.y, transform.position.z);
            EnemySpawnPoint2.position = new Vector3(player.bawah + 2, transform.position.y, transform.position.z);
            StartCoroutine(SpawnerEnemy());

        }
    }
    void CharacterSorting()
    {
        
        characters.Sort(SortPos);
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
        waveStart = true;
    }
    public void EndWave()
    {
        if(enemies.Count == 0)
        {
            waveStart = false;
            PlayerManager.instance.playerMV.BarrierOff();
            gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnerEnemy()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1,6));
        
        if(enemySpawned <= 5)
        {
            SpawnEnemy();
            StartCoroutine(SpawnerEnemy());
        }
    }
    public void EnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
        EndWave();
    }
}
