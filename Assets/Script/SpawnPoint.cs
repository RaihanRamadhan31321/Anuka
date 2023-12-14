using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public TriggerBarrier defaultTriggerBarier;
    private void OnEnable()
    {
        GameManager.Instance.spawnPoint = this.transform;
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
