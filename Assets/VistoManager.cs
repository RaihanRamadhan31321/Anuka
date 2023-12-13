using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VistoManager : MonoBehaviour
{
    public float timer = 23;
    public string nextScene;


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
        timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape) || timer <= 0)
        {
            // Ganti scene ke scene berikutnya
            SceneManager.LoadScene(nextScene);
        }
    }
}
