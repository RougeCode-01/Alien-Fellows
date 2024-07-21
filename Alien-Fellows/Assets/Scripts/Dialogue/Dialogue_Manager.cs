using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button nextButton;
    public GameObject dialoguePanel;
    public KeyCode nextKey = KeyCode.E;  // Key to progress dialogue

    private Queue<string> sentences;
    private bool isDialogueActive = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(DisplayNextSentence);
        dialoguePanel.SetActive(false);  // Ensure the panel is hidden at the start
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);  // Show the panel when dialogue starts

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(nextKey))
        {
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);  // Adjust this value to control the typing speed
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);  // Hide the panel when dialogue ends
    }
}