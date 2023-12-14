using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public LoadSceneTransition sceneTransition;
    public GameObject SettingsPanel;
    public GameObject ControlPanel;
    public AudioManager audioManager;
    public Button continueBtn;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()    
    {
        sceneTransition = FindObjectOfType<LoadSceneTransition>();
    }
    void Update()
    {
        Esc();
        if(File.Exists(Application.persistentDataPath + "/SaveData.gg"))
        {
            continueBtn.interactable = true;
        }
        else
        {
            continueBtn.interactable= false;
        }
    }

    public void PrologueScene()
    {
        SceneManager.LoadScene("LevelScene");
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

    public void WipeData()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData.gg"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData.gg");
            GameManager.Instance.levelUnlock = 1;
        }
            GameManager.Instance.levelUnlock = 1;
       
    }
    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Hapus");
    }
    public void ContinueGame()
    {
        GameManager.Instance.LoadPlayer();
    }


    /*    AudioManager audioManager;
        private void Awake()
        {
            audioManager = gameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }

        AudioManager.PlaySFX(AudioManager.);*/
}
