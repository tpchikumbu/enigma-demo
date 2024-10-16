using static Platformer.Core.Simulation;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Platformer.Mechanics {
    public class Inventory : MonoBehaviour
    {
        public static event Action<List<InventoryItem>> OnInventoryChange;
        public static event Action<InventoryItem> OnMagicSelect;
        public static event Action<ItemData> OnMagicLearned;
        public Dictionary<ItemData, InventoryItem> items = new Dictionary<ItemData, InventoryItem>();
        public List<ItemData> magicList = new List<ItemData>();
        public ItemData magicItem;
 
        private void OnEnable()
        {
            KeyInstance.OnKeyCollected += AddItem;
            MagicInstance.OnMagicCollected += AddItem;

        }

        private void OnDisable()
        {
            KeyInstance.OnKeyCollected -= AddItem;
            MagicInstance.OnMagicCollected -= AddItem;
        }

        
        public void AddItem(ItemData itemData, int amount = 1)
        {
            // If the item is already in the inventory, increase the quantity.
            if ((itemData != null) && items.ContainsKey(itemData))
            {
                items[itemData].AddQuantity();
                Debug.Log("Added " + amount + " " + itemData.itemName + " to inventory.");
                OnInventoryChange?.Invoke(items.Values.ToList());
            }
            else if (itemData != null)
            {
                items.Add(itemData, new InventoryItem(itemData, amount));
                Debug.Log("Increased count of " + itemData.itemName + " to " + amount);
                OnInventoryChange?.Invoke(items.Values.ToList());
            }
            
            if ((itemData != null) && itemData.itemCategory == "Magic"){
                magicList.Add(itemData);
                magicItem = itemData;
                OnMagicSelect?.Invoke(items[itemData]);
            }
        }

        public void RemoveItem(ItemData itemData, int amount = 1)
        {
            if (items.ContainsKey(itemData))
            {
                items[itemData].RemoveQuantity(amount);
                if (items[itemData].quantity <= 0)
                {
                    items.Remove(itemData);
                }
                OnInventoryChange?.Invoke(items.Values.ToList());
            }
        }

        public void UseItem(ItemData itemData){
            if (items.ContainsKey(itemData)){
                if (itemData.itemCategory == "Magic"){
                    Debug.Log("Using magic item");
                    OnMagicLearned?.Invoke(itemData);
                    RemoveItem(itemData);
                } else {
                    Debug.Log("Using item");
                    RemoveItem(itemData);
                }
            }
        }

        public void EvolveMagic(ItemData itemData){
            if (magicList.Contains(itemData)){
                magicList.Remove(itemData);
            }
        }

        public void UseItems(List<ItemData> items){
            foreach (ItemData item in items){
                UseItem(item);
            }
        }
    }
}
 
