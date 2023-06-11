using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Loads the scene associated with the number in the build index
    public void LoadScene(int _num)
    {
        SceneManager.LoadScene(_num);
    }

    //Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene(3);
    }
}
