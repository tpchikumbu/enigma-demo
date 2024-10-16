using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 2f, -10f); // Adjusted offset to show more of the top
    private float smoothTime = 0.35f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
