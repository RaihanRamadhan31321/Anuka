using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NpcSFX : MonoBehaviour
{

    public AudioClip npcMotor;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.Instance.SFXSource.loop = true;

            if (!AudioManager.Instance.SFXSource.isPlaying)
            {
                 NpcMotor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(AudioManager.Instance != null)
            {
                AudioManager.Instance.SFXSource.loop = false;

            }
        }
    }
    public void NpcMotor()
    {
        AudioManager.Instance.PlaySFX(npcMotor);
        Debug.Log("suaramotor");
    }
}
