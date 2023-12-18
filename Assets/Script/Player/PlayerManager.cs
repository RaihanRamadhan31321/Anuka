using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public float health = 100;
    public PlayerAttack playerATK;
    public PlayerHealthPoint playerHP;
    public PlayerMovement playerMV;
    //public CoinsUI coin;
    public bool death;
    public bool deadCheck = true;
    public static PlayerManager instance;
    public UnityEvent playerSpawn;
    


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerATK = GetComponent<PlayerAttack>();
        playerHP = GetComponent<PlayerHealthPoint>();
        playerMV = GetComponent<PlayerMovement>();
        //coin = FindObjectOfType<CoinsUI>();
        



        UIManager.instance.UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHP.currentHealth <= 0 && deadCheck == true)
        {
            StartCoroutine(DeathCounter());
        }
    }
    
    IEnumerator DeathCounter()
    {
        deadCheck = false;
        playerMV.animator.SetBool("isDead", true);
        playerMV.animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        Debug.Log("CEKKKK");
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.9f);
        playerMV.animator.updateMode = AnimatorUpdateMode.Normal;
        death = true;
    }


}
