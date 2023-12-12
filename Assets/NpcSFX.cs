using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum audioPlay
{
    MOTOR,
    MOBIL
}
public class NpcSFX : MonoBehaviour
{
    public audioPlay playThis;
    private bool motorPlay;
    public AudioClip npcMotor;
    public AudioClip npcMobil;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.Instance.NPCSource.loop = true;

            if (!AudioManager.Instance.NPCSource.isPlaying)
            {
                 NpcAudioPlay();
            }
            else
            {
                switch (playThis)
                {
                    case audioPlay.MOTOR:
                        if (motorPlay)
                        {
                            return;
                        }
                        else
                        {
                            NpcAudioPlay();
                        }
                        break;

                    case audioPlay.MOBIL:
                        if (motorPlay)
                        {
                            NpcAudioPlay();
                        }
                        else
                        {
                            return;
                        }
                        break;
                }
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
    public void NpcAudioPlay()
    {
        switch(playThis)
        {
            case audioPlay.MOTOR:
                AudioManager.Instance.PlayNPC(npcMotor);
                motorPlay = true;
                break;
            case audioPlay.MOBIL:
                AudioManager.Instance.PlayNPC(npcMobil);
                motorPlay = false;
                break;
        }
    }
}
