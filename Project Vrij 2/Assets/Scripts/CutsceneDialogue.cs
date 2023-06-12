using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float displayTime = 2f;
    public GameObject textBackground;
    public Cutscene cScene;

    private int currentDialogueIndex = 0;
    private Coroutine displayCoroutine;
    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            StartDialogue();
        }
    }

    private void Update()
    {
        // Check for button press to proceed to next dialogue
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
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
        if (currentDialogueIndex < cScene.dialogue.Length)
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
        // Display the text and enable the text background
        dialogueText.text = cScene.dialogue[currentDialogueIndex];
        textBackground.SetActive(true);

        // Start a coroutine to automatically switch to next dialogue after displayTime seconds
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
        // Hide the text and disable the text background
        dialogueText.text = "";
        textBackground.SetActive(false);
    }
}

[System.Serializable]
public class Cutscene
{
    public string name;
    public string[] dialogue;
}