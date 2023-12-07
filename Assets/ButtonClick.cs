using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    protected Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    protected virtual void OnEnable()
    {
        btn.onClick.AddListener(PlayButtonSound);
    }

    protected virtual void OnDisable()
    {
        btn.onClick.RemoveListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        if (buttonSound != null)
        {
            AudioManager.Instance.PlaySFX(buttonSound);
        }
        else
        {
            AudioManager.Instance.ButtonSound();
        }
    }
}
