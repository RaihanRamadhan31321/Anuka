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
    public GameObject ControlPanel;
    public AudioManager audioManager;

    void Update()
    {
        Esc();
    }

    public void PrologueScene()
    {
        SceneManager.LoadScene("PrologueScene");
    }

    public void ExitBoong()
    {
        SceneManager.LoadScene("ExitBoongScene");
    }

    public void Esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ControlPanel.activeSelf)
            {
                // Jika panel pengaturan aktif, matikan panel
                ControlPanel.SetActive(false);
                SettingsPanel.SetActive(true);
            }
            else
            
                if (SettingsPanel.activeSelf)
                {
                    // Jika panel pengaturan aktif, matikan panel
                    SettingsPanel.SetActive(false);
                }
            
            else
            {
                // Jika panel pengaturan dimatikan dan berada di menu utama, pindah ke ExitScene
                if (SceneManager.GetActiveScene().name == "MainMenu")
                {
                    SceneManager.LoadScene("ExitScene");
                }
                else
                {
                    // Jika bukan di menu utama, matikan panel pengaturan
                    SettingsPanel.SetActive(false);
                }
            }
        }
    }

    public void ClickSFX()
    {
        audioManager.PlaySFX(audioManager.click);
    }




}
