using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//No touching this script without letting me know please (^.^)
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public CinematicManager _cinematicManager;

    private bool isPaused = false;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    instance = managerObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayCinematic();
    }

    private void Update()
    {
        //if(isPaused)
        //{
        //    Debug.Log("Game is paused.");
        //}
        //if(!isPaused)
        //{
        //    Debug.Log("Game is not paused.");
        //}
    }

    #region PlayingCinematic

    private void PlayCinematic()
    {
        //Pauses the game
        //PauseGame();

        //Play your cinematic event
        _cinematicManager.PlayCinematic();

        //Resume game at end of cinematic with this line of code
        //ResumeGame();
    }

    #endregion

    #region Pausing

    //Call this to pause the game for a x amount of seconds
    public void PauseForSeconds(float duration)
    {
        StartCoroutine(PauseCoroutine(duration));
    }

    //Pauses the game for x amount of seconds
    private IEnumerator PauseCoroutine(float duration)
    {
        //Pause the game
        Time.timeScale = 0f;
        isPaused = true;

        yield return new WaitForSecondsRealtime(duration);

        //Resume the game
        Time.timeScale = 1f;
        isPaused = false;
    }

    //Call this to check if the game is paused or not
    public bool IsGamePaused()
    {
        return isPaused;
    }

    //Call this to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f;

        isPaused = true;
    }

    //Call this to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f;

        isPaused = false;
    }
    #endregion
}
