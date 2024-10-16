using static Platformer.Core.Simulation;
using UnityEngine;
using System;

namespace Platformer.Mechanics {
    [Serializable]
    public class InventoryItem
    {
        public ItemData itemData;
        public int quantity = 0;

        public InventoryItem(ItemData itemData, int amount = 1)
        {
            this.itemData = itemData;
            this.quantity = amount;
        }

        public void AddQuantity(int quant = 1)
        {
            this.quantity += quant;
        }

        public void RemoveQuantity(int quant = 1)
        {
            this.quantity -= quant;
        }
        
    }    
}

