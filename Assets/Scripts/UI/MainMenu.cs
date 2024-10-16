using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.SetInt("currentLevel", 4);
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 4);
        SceneManager.LoadScene(currentLevel);
        Debug.Log("Game started!");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        // Application.Quit();
        Debug.Log("Game quit!");
    }
}
