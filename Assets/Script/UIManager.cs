using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Image fillImage;

    [Header("-----------Game Objectt------------")]
    public GameObject pausePanel;//!!!!!!!!!!
    public GameObject GameOverPanel;
    public GameObject SettingsPanel;
    public GameObject ControlMapPanel;
    public GameObject CoinPanel;

    [SerializeField] private bool isPaused = true;
    private PlayerMovement player;
    private CursorController cursorController;
    //private LoadSceneTransition loadSceneTransition;




    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        cursorController = FindObjectOfType<CursorController>();
        //loadSceneTransition = FindObjectOfType<LoadSceneTransition>();
        //pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsPanel.activeSelf)
            {
                // Jika panel pengaturan terbuka, kembalikan ke panel pause
                SettingsPanel.SetActive(false);
                CoinPanel.SetActive(false);
                pausePanel.SetActive(true);

            }
            if (ControlMapPanel.activeSelf)
            {
                // Jika panel pengaturan terbuka, kembalikan ke panel pause
                ControlMapPanel.SetActive(false);
                CoinPanel.SetActive(false);
                pausePanel.SetActive(true);
                SettingsPanel.SetActive(true);

            }
            else
            {
                // Jika panel pengaturan tidak terbuka, jalankan logika pause/resume
                if (!isPaused)
                {
                    PauseGame();
                    cursorController.csr = true;
                }
                else
                {
                    ResumeGame();
                    cursorController.csr = false;
                }
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        CoinPanel.SetActive(false);
        Time.timeScale = 0;
        cursorController.csr = true;
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        CoinPanel.SetActive(true);
        cursorController.csr = false;
    }
    public void Death()
    {
        isPaused = true;
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    public void UpdateHealth(float value)
    {
        sliderHealth.value = value;

    
        float healthPercentage = sliderHealth.value;

        if (healthPercentage <= 30f) 
        {
            fillImage.color = Color.red; 
        }
        //else
        //{
        //    fillImage.color = Color.green; 
        //}

        if (sliderHealth.value <= sliderHealth.minValue)
        {
            fillImage.enabled = false;
        }
    }
    
    public void Respawn()
    {
        //loadSceneTransition.reload = true;
        GameOverPanel.SetActive(false);
        ResumeGame();
    }

    public void MenuUtama()
    {
        Time.timeScale = 1;
        //loadSceneTransition.loadMain = true;
    }


}



