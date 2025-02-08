using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Static Reference
    static public DialogueManager dm;

    // Dialogue Box Variables
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;
    private Coroutine currentTypingCoroutine = null;

    // Flags
    [HideInInspector] public bool isSpeaking;

    private void Start()
    {
        // Initialize variables
        dm = GetComponent<DialogueManager>();
        sentences = new Queue<string>();
        isSpeaking = false;
    }

    // Calls coroutine to begin displaying text to the dialogue box
    public void StartDialogue(Dialogue dialogue)
    {
        isSpeaking = true;                                                  // Update flag to show that someone is speaking
     
        // Stop the coroutine if someone is already speaking
        if (currentTypingCoroutine != null) 
        {
            StopCoroutine(currentTypingCoroutine);
            currentTypingCoroutine = null;
        }

        StopAllCoroutines();
        dialogueText.text = "";

        // Display the dialogue box UI
        animator.SetBool("FadeIn", true);
        nameText.text = dialogue.speakerName;
        nameText.color = dialogue.textColor;
        dialogueText.color = dialogue.textColor;

        // Add sentences to queue to be displayed
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        StartCoroutine(DisplaySentences());
    }

    // Prints out an entire dialogue, one sentence at a time
    IEnumerator DisplaySentences()
    {
        while (sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            currentTypingCoroutine = StartCoroutine(TypeSentence(sentence));
            yield return currentTypingCoroutine;
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(2f);
        EndDialogue();
    }

    // Prints out a sentence, one character at a time
    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.1f);
        }
        currentTypingCoroutine = null;
    }

    // Fades out the dialogue box after the dialogue is finished
    void EndDialogue()
    {
        animator.SetBool("FadeIn", false);                                  // Fade out UI
        isSpeaking = false;                                                 // Update flag to show that no one is speaking
        currentTypingCoroutine = null;                                      // End dialogue coroutine

        // If the dialogue is the 'Title Screen' dialogue, update flag to load next level
        if (TitleManager.tm != null) TitleManager.tm.dialogueFinished = true;

        // If the dialogue is after collecting a treasure, update flag to load next level
        if (MazeManager.mm != null && MazeManager.mm.treasureCollected) MazeManager.mm.dialogueFinished = true;
    }
}