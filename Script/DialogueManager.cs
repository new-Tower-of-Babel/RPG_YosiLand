using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IPointerDownHandler
{
    public Text dialogueText;
    public GameObject nextText;

    public CanvasGroup dialogueGroup;
    public Queue<string> sentences;
    private string currentSentence;
    public float typingSpeed = 0.1f;
    public bool istyping;
    public static DialogueManager instance;
    [SerializeField]
    private ActionController actionController;
    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        sentences = new Queue<string>();
    }
    private void Update()
    {
        if (dialogueText.text.Equals(currentSentence))
        {
            nextText.SetActive(true);
            istyping = false;
        }
    }

    public void OnDialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialogueGroup.alpha = 1;
        dialogueGroup.blocksRaycasts = true;
        NextSentences();
    }
    public void NextSentences()
    {
        if (sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue();
            istyping = true;
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentence));
        }
        else
        {
            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;
            actionController.dialogueImage.SetActive(false);
        }
    }
    IEnumerator Typing(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!istyping)
            NextSentences();
    }
}
