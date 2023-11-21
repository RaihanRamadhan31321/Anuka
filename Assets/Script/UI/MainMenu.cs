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

    public void ExitBoong()
    {
        SceneManager.LoadScene("ExitScene");
    }

    public void ClickSFX()
    {
        audioManager.PlaySFX(audioManager.click);
    }




}
