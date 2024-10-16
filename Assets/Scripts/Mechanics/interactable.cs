using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class interactable : MonoBehaviour
{
    public bool isInRange;
    public string interactMessage;
    public KeyCode interactKey;
    public UnityEvent interactAction; 

    //dimensions for the panel
    [SerializeField] public float panelWidth = 150;
    [SerializeField] public float panelHeight = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;

            // Create a canvas with a text object to display the interact message
            GameObject canvas = new GameObject("InteractCanvas");
            Canvas c = canvas.AddComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceCamera;
            CanvasScaler cs = canvas.AddComponent<CanvasScaler>();
            cs.dynamicPixelsPerUnit = 10;
            canvas.AddComponent<GraphicRaycaster>();
            Debug.Log("Canvas created");

            // Add panel object to canvas
            GameObject panelObject = new GameObject("InteractPanel");
            panelObject.transform.SetParent(canvas.transform);
            RectTransform panelRectTransform = panelObject.AddComponent<RectTransform>();
            panelRectTransform.localPosition = Vector3.zero;
            panelRectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);
            Image panelImage = panelObject.AddComponent<Image>();
            panelImage.color = Color.white;
            Debug.Log("Panel created");

            // Add text object to panel
            GameObject textObject = new GameObject("InteractText");
            textObject.transform.SetParent(panelObject.transform);
            TMPro.TextMeshProUGUI text = textObject.AddComponent<TMPro.TextMeshProUGUI>();
            text.text = interactMessage;
            text.font = Resources.Load<TMPro.TMP_FontAsset>("Fonts & Materials/PressStart2P-Regular SDF");
            text.alignment = TMPro.TextAlignmentOptions.Center;
            text.color = Color.black;
            text.fontSize = 8;
            Debug.Log("Text created");

            // Set text object's RectTransform properties
            RectTransform textRectTransform = text.GetComponent<RectTransform>();
            textRectTransform.localPosition = Vector3.zero;
            textRectTransform.sizeDelta = new Vector2(panelWidth - 10, panelHeight - 10);
            Debug.Log("Text moved");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Destroy(GameObject.Find("InteractCanvas"));
        }
    } 
}
