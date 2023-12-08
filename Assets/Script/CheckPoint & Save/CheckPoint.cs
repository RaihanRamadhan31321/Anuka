using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    //public PlayerManager PlayerManager;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.LoadPlayer();
            Debug.Log("Data Load");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            GameManager.Instance.cpCheck = true;
            GameManager.Instance.currentLevel = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.SavePlayer();

        }
    }
}
