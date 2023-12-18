using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum character
{
    SINGA,
    DITO
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject dito, singa, singaComp, ditoComp;
    public Transform spawnPoint;
    public Transform spawnPointCompanion;
    public character currentCharacter;
    public bool ditoUnlocked = false;
    public bool cpCheck;
    public int currentLevel = 1;
    public int levelUnlock = 1;
    public UnityAction<GameObject> onEnemyDeath;

    public UnityEvent onPlayerSpawn;

    private void Start()
    {

    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
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
                Instantiate(singa, spawnPoint.position, spawnPoint.rotation);
                if(SceneManager.GetActiveScene().buildIndex == 9)
                {
                    Instantiate(ditoComp, spawnPointCompanion.position, spawnPointCompanion.rotation);
                }
                break;
            case character.DITO:
                Instantiate(dito, spawnPoint.position, spawnPoint.rotation);
                if (SceneManager.GetActiveScene().buildIndex == 9)
                {
                    Instantiate(singaComp, spawnPointCompanion.position, spawnPointCompanion.rotation);
                }
                break;
        }
        onPlayerSpawn?.Invoke();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(PlayerManager.instance.playerMV, PlayerManager.instance.playerATK, PlayerManager.instance.playerHP, GameManager.Instance, GameManager.Instance, GameManager.Instance, GameManager.Instance);
        Debug.Log("Saving");
    }

    public void LoadPlayer()
    {
        if (SaveSystem.LoadPlayer() != null)
        {
            PlayerData data = SaveSystem.LoadPlayer();
            cpCheck = data.checkpointCheck;
            levelUnlock = data.levelUnlocks;
            ditoUnlocked = data.ditoUnlock;
            if (cpCheck)
            {
                currentLevel = data.currentLv;
                SceneManager.LoadScene(currentLevel);
                Debug.Log("yes");
                if(PlayerManager.instance != null)
                {
                    LoadPlayerCheckpoint();
                }
                else
                {
                    Invoke("LoadPlayerCheckpoint", 0.1f);
                }
                
            }
            else
            {
                SceneManager.LoadScene(1);
            }


        }
        else
        {
            return;
        }
    }
    public void LoadPlayerCheckpoint()
    {
        TriggerBarrier.FindObjectOfType<TriggerBarrier>().BarrierChange();
        PlayerData data = SaveSystem.LoadPlayer();
        PlayerManager.instance.playerHP.currentHealth = data.HP;
        PlayerManager.instance.playerMV.speed = data.Speed;
        PlayerManager.instance.playerATK.attackDamage = data.BasicAttackDMG;
        PlayerManager.instance.playerATK.specialDamage = data.SpecialAttackDMG;
        Vector3 pos;
        pos.x = data.position[0];
        pos.y = data.position[1];
        pos.z = data.position[2];


        PlayerManager.instance.playerMV.transform.position = pos;
        UIManager.instance.UpdateHealth(PlayerManager.instance.playerHP.currentHealth);

    }
    
}