using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadScene(int _num)
    {
        SceneManager.LoadScene(_num);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
