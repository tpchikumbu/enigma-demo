using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DialoguePandemoniumSystem {
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; private set; }
        protected IEnumerator WriteText(string text, float textSpeed, TMPro.TextMeshProUGUI textHolder, AudioClip soundEffect) { //Color textColor, TMPro.TMP_FontAsset textFont, float textSpeed, AudioClip soundEffect) {
            textHolder.text = "";
            print("passed text");
            // textHolder.color = textColor;
            // print("passed color");
            // textHolder.font = textFont;
            print("passed font");
            foreach(char c in text) {
                // if (Input.GetKeyDown(KeyCode.Space)) {
                //     textHolder.text = text;
                //     break;
                // }
                textHolder.text += c;
                print("writing text");
                if (soundEffect != null && SoundManagerPandemonium.instance != null && textHolder.text.Length % 10 < 4) {
                    SoundManagerPandemonium.instance.PlaySound(soundEffect);
                }
                print("played sound");
                yield return new WaitForSeconds(textSpeed);
            }
            print("finished writing text");

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            finished = true;
        }
    }

}