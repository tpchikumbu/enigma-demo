using UnityEngine;

public class PlateManager : MonoBehaviour
{
    void Update()
    {
        bool allPressed = true;
        foreach (Transform child in transform)
        {
            PressurePlate plate = child.GetComponent<PressurePlate>();
            if (plate != null && !plate.pressed)
            {
                allPressed = false;
                break;
            }
        }

        if (allPressed)
        {
            Debug.Log("All child objects have moveBack set to true.");
            Debug.Log("RELEASE THE HOUNDS! (make key rigidbody dynamic)");

            GameObject sibuKey = GameObject.Find("sibuKey");
            if (sibuKey != null)
            {
                Rigidbody2D rb = sibuKey.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
                else
                {
                    Debug.LogWarning("Rigidbody2D component not found on sibuKey.");
                }
            }
            else
            {
                Debug.LogWarning("GameObject named sibuKey not found.");
            }

            this.enabled = false;
        }
    }
}
