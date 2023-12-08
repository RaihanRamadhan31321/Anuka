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
    private bool coroutineStart = false;

    public static Dialogue instance;
    private int step;

    private void Awake()
    {
        instance = this;
    }
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
    public IEnumerator ObjekCoroutine()
    {
        coroutineStart = true;
           
        yield return new WaitForSeconds(3.5f);
        if(gameObject != null )
        {
            Destroy(gameObject);
            Debug.Log("coroutine");
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
        StartCoroutine(ObjekCoroutine());


        if (step < speaker.Length)
        {
            speakerText.text = speaker[step];
            UIImage.sprite = ImageUI[step];

        }
        else if (step == speaker.Length && coroutineStart == false)
        {
            dialogueCanvas.SetActive(false);
            interactText.SetActive(false);
            interactNextText.SetActive(false);
            interactionCompleted = true;

        }
    }


}
