using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Image fillImage;

    [Header("-----------Game Objectt------------")]
    public GameObject pausePanel;
    public GameObject saveText;
    public GameObject GameOverPanel;
    public GameObject SettingsPanel;
    public GameObject ControlMapPanel;
    public GameObject cooldownHUD;

    public bool isPaused = true;
    private bool check = true;
    private PlayerManager player;
    private CursorController cursorController;
    private LoadSceneTransition loadSceneTransition;
    [SerializeField]private Animator gameOverAnimator;

    AudioManager audioManager;

    public RectTransform dtPausePanel, dtSettings, dtControlMap;




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
                pausePanel.SetActive(false);
                cooldownHUD.SetActive(false);
                DTCancelControlMap();
            }

            else

            if (SettingsPanel.activeSelf)
            {
                // Jika panel pengaturan terbuka, kembalikan ke panel pause
                SettingsPanel.SetActive(false);
                cooldownHUD.SetActive(false);
                pausePanel.SetActive(true);
                DTCancelSettings();
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
        PauseDoTween();
        cooldownHUD.SetActive(false);
        AudioManager.Instance.NPCSource.Pause();
        Time.timeScale = 0;
        cursorController.csr = true;
    }
    public async void ResumeGame()
    {
        isPaused = false;
        await ResumeDoTween();
        pausePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        cooldownHUD.SetActive(true);
        Time.timeScale = 1;
        cursorController.csr = false;
        AudioManager.Instance.NPCSource.UnPause();
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
    public void WinToScene()
    {
        Time.timeScale = 1;
        SceneController.instance.NextLevel();
    }

    public void UpdateHealth(float values)
    {
        sliderHealth.value = values;

    
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
        Time.timeScale = 1;
        if (GameManager.Instance.cpCheck)
        {
            GameManager.Instance.LoadPlayerCheckpoint();
            PlayerManager.instance.playerHP.currentHealth = 100;
            UpdateHealth(PlayerManager.instance.playerHP.currentHealth);
        }
        else
        {

        loadSceneTransition.reload = true;
        }
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
    public IEnumerator SaveState()
    {
        if (check)
        {
            saveText.SetActive(true);
            yield return new WaitForSeconds(3f);
            saveText.GetComponent<Animator>().SetTrigger("start");
            yield return new WaitForSeconds(1f);
            saveText.SetActive(false);
            check = false;
        }
        
    }
    //DOTWEEN
    void PauseDoTween()
    {
        dtPausePanel.anchoredPosition = new Vector2(dtPausePanel.anchoredPosition.x, 1546);

        dtPausePanel.DOAnchorPosY(0, 0.7f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    async Task ResumeDoTween()
    {
        dtPausePanel.anchoredPosition = new Vector2(dtPausePanel.anchoredPosition.x, 0);

        await dtPausePanel.DOAnchorPosY(1546, 0.4f).SetUpdate(true).AsyncWaitForCompletion();
    }

    public void DTSettings()
    {
        
        dtSettings.anchoredPosition = new Vector2(dtSettings.anchoredPosition.x, 38);
        dtPausePanel.anchoredPosition = new Vector2(dtPausePanel.anchoredPosition.x, -1480);


        dtPausePanel.DOAnchorPosY(0, 0.6f).From().SetUpdate(true).OnComplete(() =>
        {
                SettingsPanel.SetActive(true);

            dtSettings.DOAnchorPosY(-934, 0.4f).SetEase(Ease.OutBack).SetUpdate(true).From().OnComplete(() =>
            {
            pausePanel.SetActive(false);
            });

        });
    }


    public void DTControlMapPanel()
    {
        ControlMapPanel.SetActive(true);
        dtControlMap.anchoredPosition = new Vector2(0, -dtControlMap.anchoredPosition.y);

        dtControlMap.DOAnchorPosX(-2093, 0.8f).SetEase(Ease.OutBack).From().SetUpdate(true).OnComplete(() =>
        {
            SettingsPanel.SetActive(false);
        });
    }

    public void DTCancelControlMap()
    {
        SettingsPanel.SetActive(true);
        dtControlMap.anchoredPosition = new Vector2(-2093, -dtControlMap.anchoredPosition.y);
        dtSettings.anchoredPosition = new Vector2(-2093, 62);

        dtControlMap.DOAnchorPosX(-2093, 0.8f).SetEase(Ease.OutBack).SetUpdate(true).From().OnComplete(() =>
        {
            ControlMapPanel.SetActive(false);
        });
        dtSettings.DOAnchorPosX(-111, 0.8f).SetEase(Ease.OutBack).SetUpdate(true);
        dtSettings.DOAnchorPosY(62, 0.8f).SetUpdate(true);
    }

    public void DTCancelSettings()
    {
        dtSettings.anchoredPosition = new Vector2(dtSettings.anchoredPosition.x, -1123);
        dtPausePanel.anchoredPosition = new Vector2(dtPausePanel.anchoredPosition.x, 0);

        dtSettings.DOAnchorPosY(62, 0.8f).SetEase(Ease.OutBack).SetUpdate(true).From().OnComplete(() =>
        {
            SettingsPanel.SetActive(false);
        });
        dtPausePanel.DOAnchorPosY(-1480, 0.8f).SetEase(Ease.OutBack).SetUpdate(true).From();
    }
}




