using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    private float previousTimeScale;

    public void PlayCinematic()
    {
        //Pause the game
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        //Play your cinematic event
        //.............

        ResumeGame();
    }

    public void PauseForSeconds(float duration)
    {
        StartCoroutine(PauseCoroutine(duration));
    }

    private IEnumerator PauseCoroutine(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);

        //Resume the game
        Time.timeScale = previousTimeScale;
    }

    public void ResumeGame()
    {
        //Resume the game
        Time.timeScale = previousTimeScale;
    }
}
