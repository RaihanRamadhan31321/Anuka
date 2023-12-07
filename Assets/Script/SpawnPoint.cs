using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
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
        AudioManager.Instance.Gameplay();
    }
}
