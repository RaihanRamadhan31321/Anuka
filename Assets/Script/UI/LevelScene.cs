using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelScene : MonoBehaviour
{
    public GameObject levelPanel;
    public CursorController cursorController;
    private LoadSceneTransition transition;
    public Button switchArrowL;
    public Button switchArrowR;
    public GameObject singa, dito;
    public TMP_Text[] namaChar;


    private void Start()
    {
        GameManager.Instance.currentCharacter = character.SINGA;
        cursorController = FindObjectOfType<CursorController>();
        transition = FindObjectOfType<LoadSceneTransition>();
        cursorController.csr = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            KeMainMenu();
        }
    }


    public void KeMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    // public void KeLevel1()
    // {
    //     Time.timeScale = 1;
    //     levelPanel.SetActive(false);
    //     //SceneManager.LoadScene("Scene Visto");
    //     transition.loadNext = true;
    //     cursorController.csr = false;
    // }


    public void SwitchCharacter()
    {
        if (GameManager.Instance.ditoUnlocked)
        {
            switchArrowL.interactable = true;
            switchArrowR.interactable = true;
            Debug.Log("CEK");
            if (singa.gameObject.activeSelf)
            {
                singa.SetActive(false);
                namaChar[0].text = "DITO";

            }
            else
            {
                singa.SetActive(true);
                GameManager.Instance.currentCharacter = character.SINGA;
                namaChar[0].text = "SINGA";
            }




            if (dito.gameObject.activeSelf)
            {
                dito.SetActive(false);
            }
            else
            {
                dito.SetActive(true);
                GameManager.Instance.currentCharacter = character.DITO;
            }
        }
        else
        {
            switchArrowL.interactable = false;
            switchArrowR.interactable = false;
        }
        


    }

}
