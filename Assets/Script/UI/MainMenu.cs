using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public LoadSceneTransition sceneTransition;
    public GameObject SettingsPanel;
    public GameObject ControlPanel;
    public GameObject mainMenuPanel;
    public AudioManager audioManager;
    public Button continueBtn;

    [Header("DoTween")]
    public RectTransform dtSettings;
    public RectTransform dtMainmenu;
    public RectTransform dtNewData;





    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()    
    {
        mainMenuPanel.SetActive(true);
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
                    mainMenuPanel.SetActive(true);
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
    public void DTSettingsPanel()
    {
        // Menetapkan posisi sebelum animasi
        dtSettings.anchoredPosition = new Vector2(-111, 62);
        dtMainmenu.anchoredPosition = new Vector2(1934, dtMainmenu.anchoredPosition.y);

        // Animasi menggunakan fungsi From untuk merubah posisi
        dtSettings.DOAnchorPosX(1473, 0.8f).SetEase(Ease.OutBack).From();
        dtSettings.DOAnchorPosY(849, 0.8f).SetEase(Ease.OutBack).From();
        dtMainmenu.DOAnchorPosX(0, 0.4f).From().OnComplete(() =>
        {
            mainMenuPanel.SetActive(false);
        });
    }

    public void DTCancelSettingPanel()
    {

        dtSettings.anchoredPosition = new Vector2(1473, 849);
        dtMainmenu.anchoredPosition = new Vector2(0, dtMainmenu.anchoredPosition.y);

        dtSettings.DOAnchorPosX(-111, 0.8f).SetEase(Ease.OutBack).From();
        dtSettings.DOAnchorPosY(62, 0.8f).SetEase(Ease.OutBack).From().OnComplete(() =>
        {
            SettingsPanel.SetActive(false);
        });
        dtMainmenu.DOAnchorPosX(1934, 0.8f).SetEase(Ease.OutBack).From();

    }

    public void DTNewData()
    {
        dtNewData.anchoredPosition = new Vector2(dtNewData.anchoredPosition.x, 0);

        dtNewData.DOAnchorPosY(-959, 0.5f).SetEase(Ease.OutBack).From().OnComplete(() =>
        {
            mainMenuPanel.SetActive(false);
        });
        dtMainmenu.DOAnchorPosY(1093, 0.5f);
    }
    public void DTCancelData()
    {
        mainMenuPanel.SetActive(true);
        dtMainmenu.anchoredPosition = new Vector2(dtMainmenu.anchoredPosition.x, 0);

        dtMainmenu.DOAnchorPosY(1093, 0.8f).SetEase(Ease.OutBack).From();
    }
}
