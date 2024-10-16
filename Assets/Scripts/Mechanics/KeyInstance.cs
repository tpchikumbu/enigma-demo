    using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using System;
using Unity.VisualScripting;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class contains the data required for implementing key collection mechanics.
    /// It is a MonoBehaviour and implements iCollectable.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class KeyInstance : MonoBehaviour, iCollectable
    {
        public AudioClip tokenCollectAudio;
        [Tooltip("If true, animation will start at a random position in the sequence.")]
        public bool randomAnimationStartTime = false;
        [Tooltip("List of frames that make up the animation.")]
        public Sprite[] idleAnimation, collectedAnimation;

        public static event KeyCollected OnKeyCollected;
        public delegate void KeyCollected(ItemData itemData, int amount = 1);
        public ItemData keyData;
        internal Sprite[] sprites = new Sprite[0];

        internal SpriteRenderer _renderer;

        //unique index which is assigned by the TokenController in a scene.
        internal int tokenIndex = -1;
        internal TokenController controller;
        //active frame in animation, updated by the controller.
        internal int frame = 0;
        internal bool collected = false;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            if (randomAnimationStartTime)
                frame = UnityEngine.Random.Range(0, sprites.Length);
            sprites = idleAnimation;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //only exectue OnPlayerEnter if the player collides with this token.
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) OnPlayerEnter(player);
        }

        void OnCollisionEnter2D(Collision2D other) {
            //only exectue OnPlayerEnter if the player collides with this token.
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) OnPlayerEnter(player);
        }

        void OnPlayerEnter(PlayerController player)
        {
            if (collected) return;
            //disable the gameObject and remove it from the controller update list.
            frame = 0;
            sprites = collectedAnimation;
            if (controller != null) {
                collected = true;
            }
        }

        public void Collect()
        {
            Debug.Log("Key Collected");
            
            OnKeyCollected?.Invoke(keyData);
            gameObject.SetActive(false);
        }

        public void Use() {
            Debug.Log("Key Used");
            Destroy(gameObject);
        }
    }
}