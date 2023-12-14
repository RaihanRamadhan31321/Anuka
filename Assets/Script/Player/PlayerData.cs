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
    //public int coinCount;
    public float[] position;
    public bool checkpointCheck;
    public int levelUnlocks;
    public bool ditoUnlock;
    public int currentLv;

    public PlayerData(PlayerMovement playerMV, PlayerAttack playerATK, PlayerHealthPoint playerHP, GameManager cpCheck, GameManager levelUnlocked, GameManager currentLvl, GameManager ditoUnlck) 
    {
        HP = playerHP.currentHealth;
        Speed = playerMV.speed;
        BasicAttackDMG = playerATK.attackDamage;
        SpecialAttackDMG = playerATK.specialDamage;
        position = new float[3];
            position[0] = playerMV.transform.position.x;
            position[1] = playerMV.transform.position.y;
            position[2] = playerMV.transform.position.z;
        //coinCount = coin.currentCoins;
        checkpointCheck = cpCheck.cpCheck;
        levelUnlocks = levelUnlocked.levelUnlock;
        currentLv = currentLvl.currentLevel;
        ditoUnlock = ditoUnlck.ditoUnlocked; 
    }
}
