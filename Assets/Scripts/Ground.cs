using UnityEngine;

public class Ground : MonoBehaviour
{
    public Vector3 originalPos;
    public float targetY;
    private bool isOver = false;
    private void Start()
    {
        originalPos = transform.position;
        targetY = originalPos.y - 2.5f;
    }

    void Update(){
        if (isOver)
            Move(isOver);
    }

    public void Move(bool isOver)
    {
        this.isOver = isOver;
        if (transform.position.y > targetY)
        {
            transform.Translate(0, -0.01f, 0);        
        }
        else
            isOver = false;
    }
}