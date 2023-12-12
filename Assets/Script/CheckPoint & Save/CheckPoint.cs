using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            GameManager.Instance.cpCheck = true;
            GameManager.Instance.currentLevel = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.SavePlayer();
            StartCoroutine(UIManager.instance.SaveState());
        }
    }
}
