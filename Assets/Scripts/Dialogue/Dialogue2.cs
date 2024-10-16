using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Dialogue2 : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    [TextArea(3, 10)]
    public string[] sentences;
    public float typingSpeed;
    public int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = "";
        // StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     if (textComponent.text == sentences[index])
        //     {
        //         NextSentence();
        //     }
        //     else
        //     {
        //         StopAllCoroutines();
        //         textComponent.text = sentences[index];
        //     }
        // }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textComponent.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            textComponent.text = "";
            gameObject.SetActive(false);
        }
    }
}
