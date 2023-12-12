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
            AudioManager.Instance.NPCSource.loop = true;

            if (!AudioManager.Instance.NPCSource.isPlaying)
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
                AudioManager.Instance.NPCSource.loop = false;

            }
        }
    }
    public void NpcMotor()
    {
        AudioManager.Instance.PlayNPC(npcMotor);
        Debug.Log("suaramotor");
    }
}
