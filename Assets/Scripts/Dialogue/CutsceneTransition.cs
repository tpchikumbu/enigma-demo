using UnityEngine;

public class CutsceneTransition : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    private void OnTriggerEnter2D(Collider2D other) {
        print("Triggered");
        if (other.CompareTag("Player")) {
            print("Transitioning to scene " + sceneIndex);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);

            // This is a hack to prevent the player from moving in the new scene
            // other.GetComponent<Platformer.Mechanics.PlayerController>().controlEnabled = false;
        }

    }
}
