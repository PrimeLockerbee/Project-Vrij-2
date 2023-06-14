using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroStory : MonoBehaviour
{
    public Sprite[] StoryArray;
    public Image StoryImage;
    public string SceneName;
    int StoryPage = 0;
    public void NextPage()
    {
        if (StoryPage >= StoryArray.Length)
        {
            SceneManager.LoadScene(SceneName);
        }
        else
        {
            StoryImage.sprite = StoryArray[StoryPage];
        }
        StoryPage++;
    }
}
