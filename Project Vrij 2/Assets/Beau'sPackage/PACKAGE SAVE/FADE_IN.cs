using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FADE_IN : MonoBehaviour
{
    public Image Fade;
    public AudioSource FadeAudio;
    void Start()
    {
        StartCoroutine(TransitionLevel());
    }
    IEnumerator TransitionLevel()
    {
        while (Fade.color.a > 0f)
        {
            var TempColor = Fade.color;
            TempColor.a -= 0.1f;
            FadeAudio.volume += 0.1f;
            Fade.color = TempColor;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
