
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timer = 23;
    public string nextScene;

    // Update is called once per frame
    void Update()
    {
        // Kurangi waktu timer
        timer -= Time.deltaTime;

        // Jika waktu timer habis
        if (timer <= 0)
        {
            // Ganti scene ke scene berikutnya
            SceneManager.LoadScene(nextScene);
        }
    }
}

