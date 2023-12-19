using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public TriggerBarrier tb;
    public TriggerBarrier nextTb;
    private void Start()
    {
        tb = FindObjectOfType<TriggerBarrier>();
        if (GameManager.Instance.cpCheck)
        {
            
            Invoke("Delay", 0.5f);
            PlayerManager.instance.playerMV.trigger = nextTb;
            PlayerManager.instance.playerMV.currentWave = 2;
            GameManager.Instance.LoadPlayerCheckpoint();
            PlayerManager.instance.playerHP.currentHealth = 100;
            PlayerManager.instance.playerMV.animator.SetBool("isDead", false);
            UIManager.instance.UpdateHealth(PlayerManager.instance.playerHP.currentHealth);
        }
    }
    void Delay()
    {
        tb.BarrierChange();
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
