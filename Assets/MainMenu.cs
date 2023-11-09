using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image newGame;
    public GameObject exitBoong;

    public void Gameplay()
    {
        Application.LoadLevel(2);
    }
    public void ExitBoong()
    {
        Time.timeScale = 0;
        exitBoong.SetActive(true);
    }

    public void gajadiDeh()
    {
        Time.timeScale = 1;
        exitBoong.SetActive(false);
    }
}
