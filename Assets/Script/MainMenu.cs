using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("PlayGame 方法被调用了！");
        SceneManager.LoadScene(1); 
    }
    public void QuitGame()
    { 
        Application.Quit();
    }

}
