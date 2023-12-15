using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;

    private void Awake()
    {
        buttonsToArray();

        int unlockedLevel = GameManager.Instance.levelUnlock;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        if(unlockedLevel > 3)
        {
            unlockedLevel = unlockedLevel - 1;
            for (int i = 0; i < unlockedLevel; i++)
            {
                buttons[i].interactable = true;
            }
        } else
        {
            for (int i = 0; i < unlockedLevel; i++)
            {
              buttons[i].interactable = true;
            }
        }

    }
    public void OpenLevel(int levelId)
    {
        string levelname = "Level " + levelId;
        SceneManager.LoadScene(levelname);
    }

    void buttonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

/*    public void LevelMusic()
    {
        AudioManager.Instance.Gameplay();
    }*/

    public void Vistoscane()
    {
        SceneManager.LoadScene(3);
    }
    public void Scene2()
    {
        SceneManager.LoadScene(6);
    }
    public void Scene3()
    {
        SceneManager.LoadScene(9);
    }
}
