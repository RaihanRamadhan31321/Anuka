using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    public GameObject exitBoong;
    public void gajadiDeh()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }


}
