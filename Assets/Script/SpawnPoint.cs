using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class SpawnPoint : MonoBehaviour
{
    public TriggerBarrier defaultTriggerBarier;
    public Transform spawnComp;
    private void OnEnable()
    {
        GameManager.Instance.spawnPoint = this.transform;
        if (SceneManager.GetActiveScene().buildIndex == 9)
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

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 3)
        {
            AudioManager.Instance.GameplayLev1();
        }
        else if (currentSceneIndex == 6)
        {
            AudioManager.Instance.GameplayLev2();
        }
        else if (currentSceneIndex == 9)
        {
            AudioManager.Instance.GameplayLev3();
        }
    }

}

