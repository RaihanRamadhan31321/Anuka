using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    public TriggerBarrier defaultTriggerBarier;
    public Transform spawnComp;
    private void OnEnable()
    {
        GameManager.Instance.spawnPoint = this.transform;
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            GameManager.Instance.spawnPointCompanion = spawnComp.transform;
        }
        
        GameManager.Instance.SpawnPlayer();
    }
    private void OnDisable()
    {
        GameManager.Instance.spawnPoint = null;
    }
    private void Start()
    {
        PlayerManager.instance.playerMV.trigger = defaultTriggerBarier;
        AudioManager.Instance.Gameplay();
    }
}
