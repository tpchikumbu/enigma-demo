using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics {
    public class InventoryManager : MonoBehaviour {
        public GameObject slotObj;
        public List<InventorySlot> InventorySlots = new List<InventorySlot>(5);

        private void OnEnable() {
            Inventory.OnInventoryChange += DrawInventory;
        }
        private void OnDisable() {
            Inventory.OnInventoryChange -= DrawInventory;
        }
        void ResetInventory() {
            foreach (Transform childTransform in transform) {
                Destroy(childTransform.gameObject);
            }
            InventorySlots.Clear();
        }

        void DrawInventory(List<InventoryItem> inventory) {
            ResetInventory();

            for (int i = 0; i < InventorySlots.Capacity; i++) {
                CreateInventorySlot();
            }
            for (int i = 0; i < inventory.Count; i++) {
                Debug.Log("Drawing Inventory: " + inventory[i].itemData.itemName + inventory[i].itemData.itemSprite);
                InventorySlots[i].drawSlots(inventory[i]);
            }
        }

        void CreateInventorySlot() {
            GameObject newSlot = Instantiate(slotObj);
            newSlot.transform.SetParent(transform, false);

            InventorySlot newComponent = newSlot.GetComponent<InventorySlot>();
            newComponent.clearSlot();

            InventorySlots.Add(newComponent);
        }
    }

}