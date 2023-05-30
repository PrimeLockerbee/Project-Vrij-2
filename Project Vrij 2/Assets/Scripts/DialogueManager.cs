using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private int currentDialogueIndex = 0;
    private string[] dialogues = {
        "Hello, how are you?",
        "I'm doing great! How about you?",
        "That's good to hear. Have a nice day!"
    };

    public void StartDialogue()
    {
        dialogueText.text = dialogues[currentDialogueIndex];
        currentDialogueIndex++;
    }

    public void ContinueDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        // Add any necessary actions after the dialogue ends
    }
}
