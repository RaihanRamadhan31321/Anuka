using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public Image newGame; 

 

    void Update()
    {
       if (Input.GetKey(KeyCode.Escape))
        {
           ExitBoong();
        } 
    }

    public void LevelScene()
    {
        SceneManager.LoadScene("LevelScene");
    }
    public void Gameplay()
    {
        Application.LoadLevel(2);
    }
    public void ExitBoong()
    {
        SceneManager.LoadScene("ExitScene");
    }


}
