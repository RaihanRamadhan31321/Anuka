using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    TriggerBarrier tb;
    private void Start()
    {
        tb = FindObjectOfType<TriggerBarrier>();
        if (GameManager.Instance.cpCheck)
        {
            tb.BarrierChange();
            GameManager.Instance.LoadPlayerCheckpoint();
            PlayerManager.instance.playerHP.currentHealth = 100;
            PlayerManager.instance.playerMV.animator.SetBool("isDead", false);
            UIManager.instance.UpdateHealth(PlayerManager.instance.playerHP.currentHealth);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            PlayerManager.instance.playerHP.currentHealth = 100;
            UIManager.instance.UpdateHealth(PlayerManager.instance.playerHP.currentHealth);
            GameManager.Instance.cpCheck = true;
            GameManager.Instance.currentLevel = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.SavePlayer();
            StartCoroutine(UIManager.instance.SaveState());
        }
    }
}
