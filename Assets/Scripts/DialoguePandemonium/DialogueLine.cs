using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DialoguePandemoniumSystem {
    
    public class DialogueLine : DialogueBaseClass
    {
        private TMPro.TextMeshProUGUI textHolder;
        [Header("Text Display")]
        // make the inpur field bigger
        [TextArea(3, 10)]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private TMPro.TMP_FontAsset textFont;  
        
        [Header("Text Speed")]
        [SerializeField] private float textSpeed = 0.05f;

        [Header("Sound")]
        [SerializeField] private AudioClip soundEffect;

        [Header("Character Image")]
        [SerializeField] private Image imageHolder;
        [SerializeField] private Sprite characterSprite;

        private void Awake() {
            textHolder = GetComponent<TMPro.TextMeshProUGUI>();
            textHolder.text = "";

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;

            
        }

        private void Start() {
            StartCoroutine(WriteText(input,  textSpeed, textHolder, soundEffect)); //textColor, textFont, , ));
        }

    }
}