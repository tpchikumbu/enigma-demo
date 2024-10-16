using UnityEngine;

public class SecretSwitch : MonoBehaviour
{

    // Set SpriteRenderers for the three different states of the switch
    private SpriteRenderer rend;
    public Sprite state1, state2, state3;
    public int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start(){
        rend = GetComponent<SpriteRenderer>();
        index = 1;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other){
        // Depending on the current index, the index is incremented or reset
        if (other.gameObject.CompareTag("Player")){
            if (index == 3){
                index = 1;
            } else {
                index++;
            }
        }

        if (index == 1){
            rend.sprite = state1;
        } else if (index == 2){
            rend.sprite = state2;
        } else if (index == 3){
            rend.sprite = state3;
        }
    }    
}
