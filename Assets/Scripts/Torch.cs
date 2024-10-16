using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]

public class Torch : MonoBehaviour
{

    private SpriteRenderer rend;
    public Sprite offSprite, onSprite; 
    
    public int switchIndex = 0;

    void Start(){
        rend = GetComponent<SpriteRenderer>();
        switchIndex = 0;

        Collider2D torchCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = GameObject.Find("Player_inventory").GetComponent<PlayerController>().GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(torchCollider, playerCollider, true);
    }
    void OnTriggerEnter2D(Collider2D other){
        // Depending on the current switchIndex, the switchIndex is incremented or reset
        if (other.gameObject.CompareTag("Projectile") && other.gameObject.name == "fireball(Clone)"){
            switchIndex = 1;
            rend.sprite = onSprite;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Projectile") && other.gameObject.name == "ice(Clone)"){
            switchIndex = 0;
            rend.sprite = offSprite;
            Destroy(other.gameObject);
        }
    }    
}
