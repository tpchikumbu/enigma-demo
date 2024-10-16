// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Platformer.Gameplay;
// using static Platformer.Core.Simulation;
// using Platformer.Model;
// using Platformer.Core;

// namespace Platformer.Mechanics
// {
//     public class PlayerController : KinematicObject
//     {
//         public AudioClip jumpAudio;
//         public AudioClip respawnAudio;
//         public AudioClip ouchAudio;
//         public int key;

//         public float maxSpeed = 7;
//         public float jumpTakeOffSpeed = 7;
//         public float dashDistance = 5f; // Dash distance

//         public JumpState jumpState = JumpState.Grounded;
//         private bool stopJump;
//         public Collider2D collider2d;
//         public AudioSource audioSource;
//         public Health health;
//         public bool controlEnabled = true;

//         bool jump;
//         bool dash; // Dash flag
//         Vector2 move;
//         SpriteRenderer spriteRenderer;
//         internal Animator animator;
//         readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

//         public Bounds Bounds => collider2d.bounds;

//         void Awake()
//         {
//             health = GetComponent<Health>();
//             audioSource = GetComponent<AudioSource>();
//             collider2d = GetComponent<Collider2D>();
//             spriteRenderer = GetComponent<SpriteRenderer>();
//             animator = GetComponent<Animator>();
//         }

//         protected override void Update()
//         {
//             if (controlEnabled)
//             {
//                 move.x = Input.GetAxis("Horizontal");
//                 if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
//                     jumpState = JumpState.PrepareToJump;
//                 else if (Input.GetButtonUp("Jump"))
//                 {
//                     stopJump = true;
//                     Schedule<PlayerStopJump>().player = this;
//                 }

//                 // Check for dash input
//                 if (Input.GetKeyDown(KeyCode.C))
//                 {
//                     dash = true;
//                 }
//             }
//             else
//             {
//                 move.x = 0;
//             }
//             UpdateJumpState();
//             base.Update();
//         }

//         void UpdateJumpState()
//         {
//             jump = false;
//             switch (jumpState)
//             {
//                 case JumpState.PrepareToJump:
//                     jumpState = JumpState.Jumping;
//                     jump = true;
//                     stopJump = false;
//                     break;
//                 case JumpState.Jumping:
//                     if (!IsGrounded)
//                     {
//                         Schedule<PlayerJumped>().player = this;
//                         jumpState = JumpState.InFlight;
//                     }
//                     break;
//                 case JumpState.InFlight:
//                     if (IsGrounded)
//                     {
//                         Schedule<PlayerLanded>().player = this;
//                         jumpState = JumpState.Landed;
//                     }
//                     break;
//                 case JumpState.Landed:
//                     jumpState = JumpState.Grounded;
//                     break;
//             }
//         }

//         protected override void ComputeVelocity()
//         {
//             if (jump && IsGrounded)
//             {
//                 velocity.y = jumpTakeOffSpeed * model.jumpModifier;
//                 jump = false;
//             }
//             else if (stopJump)
//             {
//                 stopJump = false;
//                 if (velocity.y > 0)
//                 {
//                     velocity.y = velocity.y * model.jumpDeceleration;
//                 }
//             }

//             if (move.x > 0.01f)
//                 spriteRenderer.flipX = false;
//             else if (move.x < -0.01f)
//                 spriteRenderer.flipX = true;

//             animator.SetBool("grounded", IsGrounded);
//             animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

//             targetVelocity = move * maxSpeed;

//             // Handle dash
//             if (dash)
//             {
//                 Dash();
//                 dash = false;
//             }
//         }

//         void Dash()
//         {
//             Vector3 dashDirection = spriteRenderer.flipX ? Vector3.left : Vector3.right;
//             transform.position += dashDirection * dashDistance;
//         }

//         public enum JumpState
//         {
//             Grounded,
//             PrepareToJump,
//             Jumping,
//             InFlight,
//             Landed
//         }
//     }
// }