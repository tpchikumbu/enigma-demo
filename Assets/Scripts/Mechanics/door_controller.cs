using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class door_controller : MonoBehaviour
{
    public bool isOpen;
    public ItemData doorKey;
    private SpriteRenderer rend;
    private Collider2D coll;
    public Sprite openSprite, closedSprite; 

    void Start() {
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }
    void Update() {
        rend.sprite = isOpen ? openSprite : closedSprite;
        coll.enabled = isOpen;
        coll.isTrigger = isOpen;
    }
    
    // Update is called once per frame
    public void OpenDoor() {
        Debug.Log("Opening door");
        interactable interact = GetComponentInChildren<interactable>();
        if (doorKey != null) {
            var playerInventory = FindAnyObjectByType<Inventory>();
            if (playerInventory != null && playerInventory.items.ContainsKey(doorKey)) {
                Debug.Log("Player has key");
                interact.interactMessage = "Door opened";
                playerInventory.UseItem(doorKey);
                isOpen = !isOpen;
                rend.sprite = isOpen ? openSprite : closedSprite;
                GameObject.FindObjectOfType<AudioManager>().PlayUnlock();

                // rend.sortingOrder = isOpen ? 0 : 1;
            } else {
                Debug.Log("Player does not have key");
                interact.interactMessage = "Missing key";
            }
        } else {
            Debug.Log("Door does not require key");
            isOpen = true;
            rend.sprite = openSprite;
            interact.interactMessage = "Enter - [W]";
            GameObject.FindObjectOfType<AudioManager>().PlayUnlock();
        }
    }
}
