using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TMP_Text DialogueText;
    public GameObject DialoguePanel;
    public Sprite SpeakerSprite;
    public Image Speaker;
    public void CreateDialogueText(string text)
    {
        DialoguePanel.SetActive(true);
        Speaker.sprite = SpeakerSprite;
        StartCoroutine(PlayText(text));
    }
    IEnumerator PlayText(string text)
    {
        DialogueText.text = "";
        foreach(char letter in text)
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(5f);
        DialoguePanel.SetActive(false);
    }
}
