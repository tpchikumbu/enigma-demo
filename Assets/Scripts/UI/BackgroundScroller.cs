using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollSpeed = 0.5f;
    private float offest;
    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        offest += (Time.deltaTime * scrollSpeed)/10f;
        mat.SetTextureOffset("_MainTex", new Vector2 (offest, 0));
    }

}
