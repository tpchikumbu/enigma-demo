﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.IO;
using UnityEditor;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public int key;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 5;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 5;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        public SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;
        public bool facingLeft;

        private AttackScript attackScript;
        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            facingLeft = spriteRenderer.flipX;
            attackScript = GetComponent<AttackScript>();

            // adding the different magics if they already exist
            if (PlayerPrefs.HasKey("fireball") && PlayerPrefs.GetInt("fireball") == 1) {
                ItemData flameTome = new ItemData();
                flameTome.itemName = "Tome of Ash";
                flameTome.itemCategory = "Magic";
                flameTome.itemSprite = Resources.Load<Sprite>("Assets/book_sprites/64x64/book_image_12.png");
                flameTome.itemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Projectiles/fireball.prefab");
                attackScript.AddProjectile(flameTome);
                if  (flameTome.itemPrefab != null) {
                    print("flameTome prefab loaded");
                }
                else {
                    print("flameTome prefab not loaded");
                }
            }

            if (PlayerPrefs.HasKey("blackhole") && PlayerPrefs.GetInt("blackhole") == 1) {
                ItemData gravityTome = new ItemData();
                gravityTome.itemName = "Newton's Gospel";
                gravityTome.itemCategory = "Magic";
                gravityTome.itemSprite = Resources.Load<Sprite>("Assets/book_sprites/64x64/book_image_10.png");
                gravityTome.itemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Projectiles/blackhole.prefab");
                attackScript.AddProjectile(gravityTome);
                if  (gravityTome.itemPrefab != null) {
                    print("gravityTome prefab loaded");
                }
                else {
                    print("gravityTome prefab not loaded");
                }
            }
            // add the magic projectile to the player's inventory
            

            // check which magic the player already has and set the attack script accordingly from player
            // previously learned magic
            
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y *= model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)  // Moving to the right
                spriteRenderer.flipX = facingLeft = false;
            else if (move.x < -0.01f) // Moving to the left
                spriteRenderer.flipX = facingLeft = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Crate"))
            {
                CrateMovement crate = collision.gameObject.GetComponent<CrateMovement>();
                if (crate != null)
                {
                    Vector2 pushDirection = collision.transform.position - transform.position;
                    pushDirection.Normalize();
                    crate.Push(pushDirection);
                }
            }
        }
    }
}