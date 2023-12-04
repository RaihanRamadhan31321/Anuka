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

    private bool playerInRange;
    private bool dialogueActive;

    private void Start()
    {
        dialogueCanvas.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !dialogueActive)
        {
            StartCoroutine(ShowDialogueForSeconds(3f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private IEnumerator ShowDialogueForSeconds(float seconds)
    {
        dialogueActive = true;
        dialogueCanvas.SetActive(true);
        speakerText.text = speaker[0];
        UIImage.sprite = ImageUI[0];

        yield return new WaitForSeconds(seconds);

        dialogueCanvas.SetActive(false);
        dialogueActive = false;
    }
}
