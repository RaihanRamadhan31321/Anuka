using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VistoManager : MonoBehaviour
{

    private void Start()
    {
        AudioManager.Instance.PauseMusic();
    }

    private void OnDestroy()
    {
        AudioManager.Instance.UnpauseMusic();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Level1();
        }
    }
    public void Level1()
    {
        SceneManager.LoadScene(3);
        Debug.Log("Teks");
    }
}
