using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    public void PlayCinematic()
    {
        //Pauses the game
        GameManager.Instance.PauseGame();

        //Play your cinematic event

        //Resume game at end of cinematic with this line of code
        //GameManager.Instance.ResumeGame();
    }


}
