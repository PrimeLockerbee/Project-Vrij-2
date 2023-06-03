using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float displayTime = 2f;
    //public GameObject nextButton;
    public GameObject textBackground;
    public string[] dialogue;

    private int currentDialogueIndex = 0;
    private Coroutine displayCoroutine;

    private void Start()
    {
        //Start the dialogue
        StartDialogue();
    }

    private void Update()
    {
        //Check for button press to proceed to next dialogue
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextDialogue();
        }
    }

    public void StartDialogue()
    {
        currentDialogueIndex = 0;
        DisplayDialogue();
    }

    public void DisplayNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogue.Length)
        {
            DisplayDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayDialogue()
    {
        //Display the text and enable the button
        dialogueText.text = dialogue[currentDialogueIndex];
        //nextButton.SetActive(true);

        //Start a coroutine to automatically switch to next dialogue after displayTime seconds
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
        displayCoroutine = StartCoroutine(HideDialogueAfterDelay());
    }

    private IEnumerator HideDialogueAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        DisplayNextDialogue();
    }

    private void EndDialogue()
    {
        //Hide the text and disable the button
        dialogueText.text = "";
        textBackground.SetActive(false);
        //nextButton.SetActive(false);
    }
}
