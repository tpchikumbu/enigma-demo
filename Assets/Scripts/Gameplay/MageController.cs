using UnityEngine;
using Platformer.Mechanics;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Platformer.Mechanics {
    public class MageController : MonoBehaviour
    {
        // keep the other player as a reference
        // [SerializeField] private PlayerController enigma;
        [SerializeField] private interactable textBox;

        [SerializeField] private Inventory inventory;

        [SerializeField] private string defaultText = "You have nothing to learn from me. Go and explore.";
        [SerializeField] private int[] defaultTextBoxSize = {200, 100};

        // make a dictionary of text that will be displayed when the player gets a book or a key
        [SerializeField] private Dictionary<string, string> text = new Dictionary<string, string>();

        // dictionary of text box size where key is string and value is a pair of integers
        private Dictionary<string, (int, int)> textBoxSize = new Dictionary<string, (int, int)>();


        // enigma is the player
        private PlayerController enigma;
        private AttackScript attackScript;
        private void Start() {
            // enigma = FindObjectOfType<PlayerController>();
            // find the text box that is a child of the wizard
            textBox = GetComponentInChildren<interactable>();
            inventory = FindAnyObjectByType<Inventory>();
            // add the text that will be displayed when the player gets a book or a key
            text.Add("book", "Are you ready to learn some magic? You have found a book! Press 'E' to learn.");
            textBoxSize.Add("book", (200, 100));
            text.Add("key", "Well, well, well. You have found a key! Now go and unlock the door.");
            textBoxSize.Add("key", (200, 100));
            text.Add("nothing", defaultText);
            textBoxSize.Add("nothing", (defaultTextBoxSize[0], defaultTextBoxSize[1]));

            enigma = FindObjectOfType<PlayerController>();
            attackScript = enigma.GetComponent<AttackScript>();

        }


        void Update() {
            // changing the text that is displayed on top of the wizard when the player gets a book or a key
            textBox.interactMessage = text[InventoryCheck()];
            textBox.panelWidth = textBoxSize[InventoryCheck()].Item1;
            textBox.panelHeight = textBoxSize[InventoryCheck()].Item2;

            // if the player has a book, the wizard will give them a power
            // if (Input.GetKeyDown(KeyCode.E)) {
            //     // if (InventoryCheck() == "book") {
            //     //     BestowPower();
            //     // }
            // }
        }

        public string InventoryCheck() {
            if (inventory.magicList.Count > 0) {
                return "book";
            }
            if (inventory.items.Count > 0) {
                return "key";
            }

            return "nothing";
        }

        public void BestowPower() {
            print("You have " + inventory.magicList.Count + " books, " + inventory.items.Count + " keys");

            if (InventoryCheck() == "book") {
                // get the first book from the list
                ItemData book = inventory.magicList[0];
                // learn the magic
                print("Learning magic: " + book.itemName);
                // add the magic to the player's list of magics
                // print(attackScript);
                attackScript.AddProjectile(book);
                inventory.EvolveMagic(book);
                
                print(book.itemName + " has been added to your list of magics.");
                if (book.itemName == "Tome of Ash") {
                    print("You have learned the magic of fireball.");
                    PlayerPrefs.SetInt("fireball", 1);

                    // start the learning cutscene
                    SceneManager.LoadScene("Cutscene2Fire");
                }
                if (book.itemName == "Newton's Gospel") {
                    print("You have learned the magic of gravity.");
                    PlayerPrefs.SetInt("blackhole", 1);
                }
                if (book.itemName == "Memories of Frost") {
                    print("You have learned the magic of ice.");
                    PlayerPrefs.SetInt("ice", 1);
                }
                if (book.itemName == "Dicaprio's Tale") {
                    print("You have learned the one true magic.");
                    PlayerPrefs.SetInt("\"magic\"", 1);
                }
            }
            
        }
    }

}
