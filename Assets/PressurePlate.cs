using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    public Vector3 originalPos;
    public bool moveBack = false, pressed = false;
    
    private void Start()
    {
        originalPos = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.name == "Player_inventory" ||  collision.transform.name == "crate_0")
        {
            if (transform.position.y > originalPos.y - 0.15f)
            {
                transform.Translate(0, -0.01f, 0);
                moveBack = false;
            }
            pressed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.transform.CompareTag("Player") || collision.transform.name == "crate_0")
        {
            moveBack = true;
            collision.transform.parent = null;
            pressed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveBack)
        {
            if (transform.position.y < originalPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                moveBack = false;
            }
        }
    }
}
