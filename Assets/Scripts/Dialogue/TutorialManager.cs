using UnityEngine;
using Platformer.Mechanics;
using System.Linq;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private string[] allowedMoves;

    [SerializeField] private Dialogue2 enigmaDialogue;
    [SerializeField] private Dialogue2 mageDialogue;
    [SerializeField] private int currentDialogueIndex = 0;
    [SerializeField] private int[] mageIndexes = {0,2,3,4,7,8,9,11,13};
    private bool tutorialComplete = false;

    [SerializeField] private PlayerController enigma;

    void Start() {
        mageDialogue.StartDialogue();
        enigmaDialogue.StartDialogue();
        enigmaDialogue.gameObject.SetActive(false);
        enigma.controlEnabled = false;
    }

    void Update()
    {
        if (currentDialogueIndex < 15) {
            // first 15 lines of dialogue
            if (Input.GetMouseButtonDown(0)) {
                // if the character is speaking then their dialogue text is non-empty
                if (mageIndexes.Contains(currentDialogueIndex)) {
                    mageDialogue.gameObject.SetActive(true);
                    if (mageDialogue.textComponent.text == mageDialogue.sentences[mageDialogue.index])
                    {
                        mageDialogue.NextSentence();
                    }
                    else
                    {
                        mageDialogue.StopAllCoroutines();
                        mageDialogue.textComponent.text = mageDialogue.sentences[mageDialogue.index];
                    }
                    enigmaDialogue.gameObject.SetActive(false);
                }
                else {
                    mageDialogue.gameObject.SetActive(false);
                    enigmaDialogue.gameObject.SetActive(true);
                    if (enigmaDialogue.textComponent.text == enigmaDialogue.sentences[enigmaDialogue.index])
                    {
                        enigmaDialogue.NextSentence();
                    }
                    else
                    {
                        enigmaDialogue.StopAllCoroutines();
                        enigmaDialogue.textComponent.text = enigmaDialogue.sentences[enigmaDialogue.index];
                    }
                }
                if (currentDialogueIndex < 15) {
                    currentDialogueIndex += 1;
                }
                else {
                    enigma.controlEnabled = true;
                    enigmaDialogue.gameObject.SetActive(false);
                    mageDialogue.gameObject.SetActive(false);
                }
            }
        }

        // after the first 15 lines
        else if (currentDialogueIndex == 15) {
            // if the user presses a movement key
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) {
                enigma.controlEnabled = true;
                enigmaDialogue.gameObject.SetActive(false);
                mageDialogue.gameObject.SetActive(true);
                mageDialogue.textComponent.text = "Good job! Now, let's move on to the prison.";
                currentDialogueIndex += 1;
                tutorialComplete = true;
            }
        }

    }
}
