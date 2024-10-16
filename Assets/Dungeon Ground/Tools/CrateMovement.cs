using UnityEngine;

public class CrateMovement : MonoBehaviour
{
    public float pushForce = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 pushDirection)
    {
        rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
    }
}
