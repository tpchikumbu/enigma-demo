using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using System;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class contains the data required for implementing key collection mechanics.
    /// It is a MonoBehaviour and implements iCollectable.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class MagicInstance : MonoBehaviour, iCollectable
    {
        public AudioClip tokenCollectAudio;
        [Tooltip("If true, animation will start at a random position in the sequence.")]
        public bool randomAnimationStartTime = false;
        [Tooltip("List of frames that make up the animation.")]
        public Sprite[] idleAnimation, collectedAnimation;

        public static event MagicCollected OnMagicCollected;
        public delegate void MagicCollected(ItemData itemData, int amount = 1);

        public static event MagicLearned OnMagicLearned;
        public delegate void MagicLearned(ItemData itemData);
        public ItemData magicData;
        internal Sprite[] sprites = new Sprite[0];

        internal SpriteRenderer _renderer;

        [SerializeField] public string magicName;

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
            _renderer.sprite = magicData.itemSprite;

            // check if magic already exists in players saved data
            if (PlayerPrefs.HasKey(magicName) && PlayerPrefs.GetInt(magicName) == 1) {
                collected = true;
                gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
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
                player.key = gameObject.GetInstanceID();
                collected = true;
            }
        }

        public void Collect()
        {
            Debug.Log("Magical item collected");
            
            OnMagicCollected?.Invoke(magicData);
            gameObject.SetActive(false);
        }

        public void Use() {
            Debug.Log("Magic item Used");

            OnMagicLearned?.Invoke(magicData);
            Destroy(gameObject);
        }
    }
}