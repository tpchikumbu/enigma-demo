using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void Start() {
        int savedLevel = PlayerPrefs.GetInt("currentLevel", 1);
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        // If the current level is greater than the saved level, update the saved level
        if (currentLevel > savedLevel) {
            PlayerPrefs.SetInt("currentLevel", currentLevel);
        }
    }
        
}
