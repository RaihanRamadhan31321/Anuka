using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public int sceneIndex; // Nama scene yang ingin dipindahkan

    void Update()
    {
        // Cek apakah tombol Enter ditekan
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Pindahkan ke scene yang diinginkan
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

