using UnityEngine;
using UnityEngine.UI;

public class DialogueButton : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        dialogueManager.ContinueDialogue();
    }
}