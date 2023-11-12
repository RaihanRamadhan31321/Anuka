using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int HP;
    public float Speed;
    public int BasicAttackDMG;
    public int SpecialAttackDMG;
    public int coinCount;
    public float[] position;

    public PlayerData(PlayerMovement playerMV, PlayerAttack playerATK, PlayerHealthPoint playerHP, CoinsUI coin) 
    {
        HP = playerHP.currentHealth;
        Speed = playerMV.speed;
        BasicAttackDMG = playerATK.attackDamage;
        SpecialAttackDMG = playerATK.specialDamage;
        position = new float[3];
            position[0] = playerMV.transform.position.x;
            position[1] = playerMV.transform.position.y;
            position[2] = playerMV.transform.position.z;
        coinCount = coin.currentCoins;
    }
}
