using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum character
{
    SINGA,
    DITO
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject dito, singa;
    public Transform spawnPoint;
    public character currentCharacter;

    public UnityEvent onPlayerSpawn;

    private void Start()
    {

    }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void SpawnPlayer()
    {
        switch (currentCharacter)
        {
            case character.SINGA:
                Instantiate (singa, spawnPoint.position, spawnPoint.rotation);
            break;
            case character.DITO:
                Instantiate (dito, spawnPoint.position, spawnPoint.rotation);
            break;
        }
        onPlayerSpawn?.Invoke();
    }
}
