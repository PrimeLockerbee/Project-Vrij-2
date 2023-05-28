using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : MonoBehaviour
{
    private PlayableDirector director;
    public PlayableAsset[] cutScenes;

    private int currentCutSceneIndex = 0;

    public bool hasPlayed = false;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (!hasPlayed && director.state == PlayState.Playing && director.time >= director.duration)
        {
            hasPlayed = true;
            director.Stop();
        }
    }

    public void PlayCinematic()
    {
        director.playableAsset = cutScenes[currentCutSceneIndex];
        director.Play();
    }

    public void NextCinematic()
    {
        currentCutSceneIndex++;

        //Check if there are anymore cutscenes
        if (currentCutSceneIndex < cutScenes.Length)
        {
            //Change the active cutscene to the next one
            director.playableAsset = cutScenes[currentCutSceneIndex];
        }
        else
        {
            //No more cutscenes left
            return;
        }
    }
}
