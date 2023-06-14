using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Dialogue
{
    public string GivenText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CreateDialogueText(GivenText);
        }
    }
}
