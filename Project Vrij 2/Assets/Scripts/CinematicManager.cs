using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : MonoBehaviour
{
    public PlayableDirector director;
    public PlayableAsset[] cutScenes;
    public Collider[] triggers;

    public int currentPlayingIndex = -1; // Track the index of the currently playing cutscene

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }


    public void PlayCinematic(int triggerIndex)
    {
        if (triggerIndex >= 0 && triggerIndex < cutScenes.Length)
        {
            if (currentPlayingIndex != triggerIndex) // Check if the cutscene is not already playing
            {
                director.Stop(); // Stop the currently playing cutscene
                director.playableAsset = cutScenes[triggerIndex];
                director.Play();
                currentPlayingIndex = triggerIndex;
            }
        }
    }
}
