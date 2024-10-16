using UnityEngine;

public class TorchManager : MonoBehaviour
{

    private readonly Torch[] torches = new Torch[3];
    public int state = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start(){
        // get torch object by name
        GameObject torchObject = GameObject.Find("Torch1");
        torches[0] = torchObject.GetComponent<Torch>();
        torchObject = GameObject.Find("Torch2");
        torches[1] = torchObject.GetComponent<Torch>();
        torchObject = GameObject.Find("Torch3");
        torches[2] = torchObject.GetComponent<Torch>();
    }
    void OnTriggerEnter2D(Collider2D other){
        // If the player enters the trigger area, the state is updated
        if (other.gameObject.CompareTag("Player")){
            state = torches[0].switchIndex + torches[1].switchIndex * 2 + torches[2].switchIndex * 4;
        }
    }
          
}
