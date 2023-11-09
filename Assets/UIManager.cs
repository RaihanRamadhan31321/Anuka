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

    public GameObject pausePanel;
    private bool isPaused = true;
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void UpdateHealth(float value)
    {
        sliderHealth.value = value;

    
        float healthPercentage = sliderHealth.value;

        if (healthPercentage <= 30f) 
        {
            fillImage.color = Color.red; 
        }
        else
        {
            fillImage.color = Color.green; 
        }

        if (sliderHealth.value <= sliderHealth.minValue)
        {
            fillImage.enabled = false;
        }
    }

    public void MenuUtama()
    {
        Application.LoadLevel(0);
    }

}



