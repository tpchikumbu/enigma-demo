using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 4;
    public int damage = 1;
    public GameObject effect;

    private Platformer.Mechanics.PlayerController player;
    private Rigidbody2D rigidbody;

    private string projectileName;

    void Start() {

        rigidbody = GetComponent<Rigidbody2D>(); 
        // print the name of the projectile
        projectileName = gameObject.name;
        print(projectileName);


        player = GameObject.Find("Player_inventory").GetComponent<Platformer.Mechanics.PlayerController>();
        
        float direction = player.facingLeft ? 1 : -1;
        
        rigidbody.linearVelocity = direction * projectileSpeed * transform.right;
        
        Collider2D projectileCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(projectileCollider, playerCollider);
        
        if (GameObject.Find("Mage Variant") != null) {
            Platformer.Mechanics.MageController Mage = GameObject.Find("Mage Variant").GetComponent<Platformer.Mechanics.MageController>();
            Collider2D MageCollider = Mage.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(projectileCollider, MageCollider);
        }

    }

    void OnCollisionEnter2D(Collision2D hitInfo) {
        if (hitInfo.gameObject.CompareTag("Crate")) {
            if (projectileName == "fireball(Clone)") {
                Destroy(hitInfo.gameObject);
                
            }
            else if (projectileName == "blackhole(Clone)") {
                // teleport the crate to the player
                float direction = player.facingLeft ? -1 : 1;
                hitInfo.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                hitInfo.gameObject.transform.position = new Vector2(player.transform.position.x + direction, player.transform.position.y);
            }
        }

        if (!hitInfo.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
        GameObject.FindObjectOfType<AudioManager>().PlayHit();
    }
}
