using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Platformer.Mechanics;

namespace DialoguePandemoniumSystem {
    public class DialogueHolder : MonoBehaviour
    {
        [SerializeField] private PlayerController enigma;

        private void Awake() {
            enigma = FindObjectOfType<PlayerController>();
            enigma.controlEnabled = false;
            StartCoroutine(dialogueSequence());
        }

        private IEnumerator dialogueSequence () {
            for (int i = 2; i < transform.childCount; i++) {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueBaseClass>().finished);
            }

            gameObject.SetActive(false);
            enigma.controlEnabled = true;
        }

        private void Deactivate() {
            for (int i = 2; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void KillDialogue() {
            StopAllCoroutines();
            gameObject.SetActive(false);
            enigma.controlEnabled = true;
        }
    }
}
