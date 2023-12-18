using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source------------")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] public AudioSource SFXSource;
    public AudioSource NPCSource;

    [Header("------------Audio Clip------------")]
    public AudioClip mainTheme;
    public AudioClip gameplayTheme;

    [Header("------------SFX Player------------")]
    public AudioClip click;
    public AudioClip footStep;
    public AudioClip missHit;
    public AudioClip missHugeHit;
    public AudioClip hitBasicAtt;
    public AudioClip hugeAtt;
    public AudioClip gameOver;


    [Header("------------SFX Player------------")]
    public AudioClip enemyHit;
    public AudioClip lowHealthHit;

    [Header("------------NPC------------")]
    public AudioClip npcAngkot;
    public AudioClip npcNgopi;
    public AudioClip npcGosip;
    public AudioClip npcProvok;

    public static AudioManager Instance;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        Mainmenu();
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlayNPC(AudioClip clip)
    {
        NPCSource.PlayOneShot(clip);
    }

    public void Gameplay()
    {
        StartCoroutine(MusicChange(gameplayTheme));
        Debug.Log("Blabla");
    }

    public void Mainmenu()
    {
        StartCoroutine(MusicChange(mainTheme));
    }

    IEnumerator MusicChange(AudioClip audio)
    {
        MusicSource.Stop();
        MusicSource.clip = audio;
        yield return new WaitForSeconds(0.1f);
        MusicSource.Play();
        Debug.Log("ganti music");
    }

    public void PauseMusic()
    {
        MusicSource.Pause();
        NPCSource.Pause();
    }

    public void UnpauseMusic()
    {
        MusicSource.UnPause();
        NPCSource.UnPause();
    }

    public void ButtonSound()
    {
        PlaySFX(click);
    }
    


}
