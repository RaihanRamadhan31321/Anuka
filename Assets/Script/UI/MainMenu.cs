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
    public LoadSceneTransition sceneTransition;
    public GameObject SettingsPanel;
    public AudioManager audioManager;

    private void Start()
    {
        sceneTransition = FindObjectOfType<LoadSceneTransition>();
    }
    void Update()
    {
        Esc();
    }

    public void PrologueScene()
    {
        sceneTransition.loadNext = true;
    }

    public void ExitBoong()
    {
        SceneManager.LoadScene("ExitBoongScene");
    }

    public void Esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
