using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source------------")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------------Audio Clip------------")]
    public AudioClip mainTheme;
    public AudioClip gameplayTheme;
    public AudioClip click;

    public static AudioManager Instance;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
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
    }


}
