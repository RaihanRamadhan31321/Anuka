using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Update()
    {
        // Jika tombol 'C' ditekan, toggle kursor
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        // Jika kursor terlihat, sembunyikan; jika tidak, tampilkan
        Cursor.visible = !Cursor.visible;

        // Setel kunci kursor sesuai dengan visibilitas kursor
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None; // Kursor dapat bergerak bebas
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Kursor terkunci di tengah layar
        }
    }
}
