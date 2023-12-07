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
    [SerializeField]private int enemySpawned;
    [SerializeField]private List<GameObject> characters;
    [SerializeField] private List<GameObject> enemies;
    // Start is called before the first frame update
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
            StartCoroutine(SpawnerEnemy());
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTriggered == false)
        {
            isFighting = true;
            waveStart = true;
            player.atas = (transform.position.x) - player.batasBarrier;
            player.bawah = transform.position.x + player.batasBarrier;
            isTriggered = true;
            EnemySpawnPoint1.position = new Vector3(player.atas - 2, transform.position.y, transform.position.z);
            EnemySpawnPoint2.position = new Vector3(player.bawah + 2, transform.position.y, transform.position.z);

        }
    }
    void CharacterSorting()
    {
        enemies = new List<GameObject> (GameObject.FindGameObjectsWithTag("Enemy"));
        characters = new List<GameObject>(enemies);
        characters.Add(player.gameObject);
        characters.Sort(SortPos);
        foreach (var character in characters)
        {
            character.GetComponent<SpriteRenderer>().sortingOrder = characters.IndexOf(character);
            //SpriteRenderer sr = character.gameObject.GetComponent<SpriteRenderer>();
            //sr.sortingOrder = characters.IndexOf(character);
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
        if (enemySpawned <= 5)
        {
            RandomNum = UnityEngine.Random.Range(1, 3);
            switch (RandomNum)
            {
                case 1:
                    Instantiate(enemy, EnemySpawnPoint1.position, EnemySpawnPoint1.rotation);
                    enemySpawned++;
                    break;
                case 2:
                    Instantiate(enemy, EnemySpawnPoint2.position, EnemySpawnPoint2.rotation);
                    enemySpawned++;
                    break;
            }
        }
    }

    IEnumerator SpawnerEnemy()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1,5));
        SpawnEnemy();
    }
}
