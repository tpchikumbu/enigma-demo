using Unity.VisualScripting;
using UnityEngine;
using System;

public class playerTeleportation : MonoBehaviour
{
    private GameObject currentTeleporter;
    void Update()
    {
        if (Input.GetButtonDown("Vertical") && currentTeleporter != null) 
        {
            if (currentTeleporter.GetComponent<basicTeleporter>()){
                transform.position = currentTeleporter.GetComponent<basicTeleporter>().GetDestination().position;
            } else {
                transform.position = currentTeleporter.GetComponent<teleporter>().GetDestination().position;
            }
            GameObject.FindObjectOfType<AudioManager>().PlayTeleport();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
            currentTeleporter = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
                currentTeleporter = null;
        }
    }
}

