using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Platformer.Mechanics {
    public class InventorySlot : MonoBehaviour {
        public Image icon;
        public TextMeshProUGUI amountText;
        public TextMeshProUGUI nameText;

        public void clearSlot() {
            icon.enabled = false;
            amountText.enabled = false ;
            nameText.enabled = false;
        }
    
        public void drawSlots(InventoryItem item) {
            if (item == null) { 
                clearSlot();
                return;
            }

            icon.enabled = true;
            amountText.enabled = true;
            nameText.enabled = true;

            icon.sprite = item.itemData.itemSprite;
            amountText.text = item.quantity.ToString();
            nameText.text = item.itemData.itemName;
        }

        public void drawMagicSlots(InventoryItem item) {
            if (item == null) { 
                clearSlot();
                return;
            }

            icon.enabled = true;
            amountText.enabled = true;
            nameText.enabled = true;

            GameObject magic = item.itemData.itemPrefab;

            icon.sprite = magic.GetComponent<SpriteRenderer>().sprite;
            amountText.text = "Damage: " + magic.GetComponent<Projectile>().damage.ToString();
            nameText.text = magic.name;
        }
    }

}