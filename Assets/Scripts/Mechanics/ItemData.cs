using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public string itemCategory;
        public Sprite itemSprite;
        public GameObject itemPrefab = null;
    }
}

