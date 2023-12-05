using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private Image UIImage;

    [SerializeField] private string[] speaker;
    [SerializeField] private Sprite[] ImageUI;

    public GameObject interactText;
    public GameObject interactNextText;

    private bool dialogueActive;
    private bool interactionCompleted;
    private bool dialogueInProgress;

    private int step;

    void Update()
    {
        if (dialogueActive && !interactionCompleted && Input.GetKeyDown(KeyCode.F) && !dialogueInProgress)
        {
            StartDialogue();
        }

        if (dialogueInProgress && Input.GetKeyDown(KeyCode.Return))
        {
            ContinueDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !dialogueInProgress)
        {
            interactText.SetActive(true);
            dialogueActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactText.SetActive(false);
        interactNextText.SetActive(false);
        dialogueCanvas.SetActive(false);
        dialogueInProgress = false;
        dialogueActive = false;
        interactionCompleted = false;
        step = 0;
    }

    private void StartDialogue()
    {
        dialogueInProgress = true;
        dialogueCanvas.SetActive(true);
        interactText.SetActive(false);
        interactNextText.SetActive(true);
        speakerText.text = speaker[step];
        UIImage.sprite = ImageUI[step];
    }

    private void ContinueDialogue()
    {
        step++;

        if (step < speaker.Length)
        {
            speakerText.text = speaker[step];
            UIImage.sprite = ImageUI[step];
        }
        else
        {
            dialogueCanvas.SetActive(false);
            interactText.SetActive(false);
            interactNextText.SetActive(false);
            interactionCompleted = true;
        }
    }
}
