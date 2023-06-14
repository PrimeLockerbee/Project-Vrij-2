using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitchActivation : MonoBehaviour
{
    public string LevelName;
    public Image TransitionScreen;
    public AudioSource FadeAudio;
    public void Start()
    {
        StartCoroutine(TransitionLevel());
    }
    IEnumerator TransitionLevel()
    {
        while (TransitionScreen.color.a < 1f)
        {
            var TempColor = TransitionScreen.color;
            TempColor.a += 0.1f;
            FadeAudio.volume -= 0.1f;
            TransitionScreen.color = TempColor;
            yield return new WaitForSeconds(0.2f);
        }
        SceneManager.LoadScene(LevelName);
    }
}
