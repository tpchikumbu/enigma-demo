using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics {
    public class MagicManager : MonoBehaviour {
        public GameObject slotObj;
        public InventorySlot InventorySlot;
        

        private void OnEnable() {
            AttackScript.OnMagicSelect += DrawInventory;
        }
        private void OnDisable() {
            AttackScript.OnMagicSelect -= DrawInventory;
        }
        void ResetInventory() {
            foreach (Transform childTransform in transform) {
                Destroy(childTransform.gameObject);
                Debug.Log("Destroying magic item: " + childTransform.gameObject);
            }
        }

        void DrawInventory(InventoryItem magicItem) {
            ResetInventory();
            CreateInventorySlot();
            Debug.Log("Drawing Magic: " + magicItem.itemData.itemName);
            InventorySlot.drawMagicSlots(magicItem);
        }

        void CreateInventorySlot() {
            GameObject newSlot = Instantiate(slotObj);
            newSlot.transform.SetParent(transform, false);

            InventorySlot = newSlot.GetComponent<InventorySlot>();
            InventorySlot.clearSlot();
        }
        
    }

}