using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingsPanel;
    public AudioManager audioManager;
    private bool isPaused = true;
    void Update()
    {
       if (Input.GetKey(KeyCode.Escape))
        {
           ExitBoong();
        } 
    }

    public void PrologueScene()
    {
        SceneManager.LoadScene("PrologueScene");
    }
    public void Gameplay()
    {
        Application.LoadLevel(2);
    }
    public void ExitBoong()
    {
        SceneManager.LoadScene("ExitScene");
    }

/*    public void ESC()
    {
        Time.timeScale;
    }*/

    public void ClickSFX()
    {
        audioManager.PlaySFX(audioManager.click);
    }




}
