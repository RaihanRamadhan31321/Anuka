
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timer = 23;
    public string nextScene;

    private void Start()
    {
        AudioManager.Instance.PauseMusic();
    }

    void Update()
    {
        // Kurangi waktu timer
        timer -= Time.deltaTime;

        // Jika waktu timer habis
        if (Input.GetKeyDown(KeyCode.Escape) || timer <= 0)
        {
            if (Time.timeScale != 0)
            {
                // Ganti scene ke scene berikutnya
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(nextScene);
            }

        }


    }
    private void OnDestroy()
    {
        AudioManager.Instance.UnpauseMusic();
    }

}

