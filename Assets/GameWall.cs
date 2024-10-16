using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameWall : MonoBehaviour
{
    GameWallManager gameWallManager;
    public int move = 0;
    public int id;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the GameWallManager object
        GameObject gameWallManagerObject = GameObject.Find("GameWallManager");
        gameWallManager = gameWallManagerObject.GetComponent<GameWallManager>();
        // Ignore collision with the player
        Collider2D wallCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = GameObject.Find("Player_inventory").GetComponent<Platformer.Mechanics.PlayerController>().GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(wallCollider, playerCollider, true);
    }

    // set up what happens when something collides with the wall
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Compare the tag of the object that collided with the wall
        if (collision.gameObject.CompareTag("Projectile")){
            move = collision.gameObject.GetComponent<Projectile>().damage;
        }
        gameWallManager.Hit(id);
    }
}


