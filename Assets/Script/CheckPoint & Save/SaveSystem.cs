using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SavePlayer(PlayerMovement playerMV, PlayerAttack playerATK, PlayerHealthPoint playerHP, GameManager cpCheck, GameManager levelUnlocked, GameManager currentLvl, GameManager ditoUnlck)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveData.gg";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerDT = new PlayerData(playerMV, playerATK, playerHP, cpCheck, levelUnlocked, currentLvl, ditoUnlck);
        formatter.Serialize(stream, playerDT);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/SaveData.gg";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData playerDT = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerDT;
        }
        else
        {
            Debug.LogError("Tidak Ada Save Data di " + path);
            return null;
        }
    }
}
