using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarahMusuh : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //animasi kena pukul

        if (currentHealth < 0)
        {
            Die();
        }
    }
    public void SpecialTakeDamage(int specialDamage)
    {
        currentHealth -= specialDamage;

        //animasi kena pukul

        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("MATI DIA");

        //animasi mati

        //disable musuh
    }
}
