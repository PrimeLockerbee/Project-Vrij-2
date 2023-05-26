using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _cinematicManager.PlayCinematic();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }

    public void LoadLevel(float buildindex)
    {
        //Implement level loading logic here
    }

    public void QuitGame()
    {
        //Implement game quitting logic here
    }


    public void RestartGame()
    {
        //Restart or load the initial level
        LoadLevel(0);
    }

}
