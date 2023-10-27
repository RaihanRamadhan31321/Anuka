using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarahPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = UIManager.instance;
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //animasi kena pukul

        if (currentHealth < 0)
        {
            Die();
        }
        _uiManager.UpdateHealth(currentHealth);
    }
    void Die()
    {
        Debug.Log("MATI PLAYER");

        //animasi mati

        //disable musuh
    }
   

}
