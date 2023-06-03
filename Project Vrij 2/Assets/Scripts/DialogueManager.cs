using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float displayTime = 2f;
    public GameObject textBackground;
    public NPC[] npcs;

    private int currentNPCIndex = 0;
    private int currentDialogueIndex = 0;
    private Coroutine displayCoroutine;

    private void Update()
    {
        // Check for button press to proceed to next dialogue
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
        if (currentDialogueIndex < npcs[currentNPCIndex].dialogue.Length)
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
        dialogueText.text = npcs[currentNPCIndex].dialogue[currentDialogueIndex];
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

    public void ChangeNPC(int npcIndex)
    {
        // Change to dialogue for the specified NPC
        currentNPCIndex = npcIndex;
        StartDialogue();
    }
}

[System.Serializable]
public class NPC
{
    public string name;
    public string[] dialogue;
}