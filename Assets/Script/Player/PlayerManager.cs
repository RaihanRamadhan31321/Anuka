using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public float health = 100;
    [SerializeField] private UIManager _uiManager;
    public PlayerAttack playerATK;
    public PlayerHealthPoint playerHP;
    public PlayerMovement playerMV;
    public CoinsUI coin;
    public bool death;
    public bool deadCheck = true;
    public static PlayerManager instance;


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
        coin = FindObjectOfType<CoinsUI>();
        


        _uiManager = UIManager.instance;
        _uiManager.UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHP.currentHealth <= 0 && deadCheck == true)
        {
            StartCoroutine(DeathCounter());
        }
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(playerMV , playerATK, playerHP, coin);
        Debug.Log("Saving");
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playerHP.currentHealth = data.HP;
        playerMV.speed = data.Speed;
        playerATK.attackDamage = data.BasicAttackDMG;
        playerATK.specialDamage = data.SpecialAttackDMG;
        Vector3 pos;
            pos.x = data.position[0];
            pos.y = data.position[1];
            pos.z = data.position[2];
        coin.currentCoins = data.coinCount;

        playerMV.transform.position = pos;
        _uiManager.UpdateHealth(playerHP.currentHealth);
        coin.coinText.text = "Coins:" + coin.currentCoins.ToString();



    }
    IEnumerator DeathCounter()
    {
        deadCheck = false;
        playerMV.animator.SetTrigger("isDead");
        playerMV.animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        Debug.Log("CEKKKK");
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.9f);
        playerMV.animator.updateMode = AnimatorUpdateMode.Normal;
        death = true;
    }


}
