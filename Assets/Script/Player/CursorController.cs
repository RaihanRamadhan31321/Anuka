using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [HideInInspector] public bool csr = false;
    void Update()
    {
        // Jika tombol 'C' ditekan, toggle kursor
        if (csr)
        {
            ToggleCursor();
            CursorOn();
        }
        else
        {
            ToggleCursor();
            CursorOf();
        }
    }

    void ToggleCursor()
    {
        // Jika kursor terlihat, sembunyikan; jika tidak, tampilkan
        //Cursor.visible = !Cursor.visible;

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

    public void CursorOn()
    {
        if (!Cursor.visible)
        {
            Cursor.visible = true;
        }
    }

    public void CursorOf()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
    }
}
