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
    private PlayerManager player;
    private CursorController cursorController;
    private LoadSceneTransition loadSceneTransition;
    [SerializeField]private Animator gameOverAnimator;

    AudioManager audioManager;


    private void Awake()
    {
        instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        player = PlayerManager.instance;
        cursorController = FindObjectOfType<CursorController>();
        loadSceneTransition = FindObjectOfType<LoadSceneTransition>();
        //pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ControlMapPanel.activeSelf)
            {
                // Jika panel pengaturan aktif, matikan panel
                ControlMapPanel.SetActive(false);
                SettingsPanel.SetActive(true);
                CoinPanel.SetActive(false);
                pausePanel.SetActive(false);
            }

            else

            if (SettingsPanel.activeSelf)
            {
                // Jika panel pengaturan terbuka, kembalikan ke panel pause
                SettingsPanel.SetActive(false);
                CoinPanel.SetActive(false);
                pausePanel.SetActive(true);

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
                if (isPaused && !ControlMapPanel.activeSelf && !SettingsPanel.activeSelf)
                {
                    ResumeGame();
                    cursorController.csr = false;
                }
            }
        }
        if (player.death)
        {
            player.playerHP.currentHealth = 100;
            player.death = false;
            Death();
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
        SettingsPanel.SetActive(false);
        CoinPanel.SetActive(true);
        cursorController.csr = false;
    }
    public void Death()
    {
        cursorController.csr = true;
        isPaused = true;
        gameOverAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        audioManager.PlaySFX(audioManager.gameOver);
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
        gameOverAnimator.updateMode = AnimatorUpdateMode.Normal;
        player.death = false;
        loadSceneTransition.reload = true;
        GameOverPanel.SetActive(false);
        ResumeGame();
    }

    public void MenuUtama()
    {
        gameOverAnimator.updateMode = AnimatorUpdateMode.Normal;
        Time.timeScale = 1;
        loadSceneTransition.loadMain = true;
        AudioManager.Instance.Mainmenu();
    }


}



