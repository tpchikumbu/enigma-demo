using Unity.VisualScripting;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        iCollectable collectable = other.GetComponent<iCollectable>();
        if (collectable != null)
        {
            collectable.Collect();
            GameObject.FindObjectOfType<AudioManager>().PlayCollect();
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        iCollectable collectable = other.gameObject.GetComponent<iCollectable>();
        if (collectable != null)
        {
            collectable.Collect();
            GameObject.FindObjectOfType<AudioManager>().PlayCollect();
        }
    }
}
