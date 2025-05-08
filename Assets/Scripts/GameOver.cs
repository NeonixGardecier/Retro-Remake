using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public int currentScene;
    public int mainMenuScene;
    public void OnRestart()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void OnQuit()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
