using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class AttackScript : MonoBehaviour {

    
    public Transform projectileStart;
    public static event Action<InventoryItem> OnMagicSelect;
    public List<ItemData> magicProjectiles = new List<ItemData>(1);
    public ItemData currentMagicInstance;
    public int projectileIdx = 0;

    void OnEnable() {
        MagicInstance.OnMagicLearned += AddProjectile;
    }

    void Start() {
        if (magicProjectiles.Count > 0) {
            currentMagicInstance = magicProjectiles[0]; // Initialize with a default MagicInstance
        }
    }

    void Update() {
        if (CompareTag("Player")) {
            if (Input.GetButtonDown("Fire1")) {
                if (currentMagicInstance.itemPrefab != null) {
                    Instantiate(currentMagicInstance.itemPrefab, projectileStart.position, projectileStart.rotation);
                } else {
                    Debug.Log("Projectile not set");
                }
                GameObject.FindObjectOfType<AudioManager>().PlayFire();
            }

            if (Input.GetButtonDown("Fire2")) {
                SwitchProjectile();
            }
        }
    }

    public void SwitchProjectile() {
        // Logic to switch the currentMagicInstance to the next one
        Debug.Log("Switching projectile");
        projectileIdx = (projectileIdx + 1) % magicProjectiles.Count;
        currentMagicInstance = magicProjectiles[projectileIdx];
        OnMagicSelect?.Invoke(new InventoryItem(currentMagicInstance));
    }

    public void AddProjectile(ItemData itemData) {
        // Add the projectile to the dictionary
        Debug.Log("Adding projectile to the list: " + itemData.itemName);
        magicProjectiles.Add(itemData);

        for (int i = 0; i < magicProjectiles.Count; i++) {
            Debug.Log("Projectile " + i + ": " + magicProjectiles[i].itemName);
        }
    }

    public void Shoot(int index) {
        // Delay for 2 seconds before shooting the projectile
        // System.Threading.Thread.Sleep(2000);
        Debug.Log("Shooting projectile: " + magicProjectiles[index].itemName);
        Instantiate(magicProjectiles[index].itemPrefab, projectileStart.position, projectileStart.rotation);
        GameObject.FindObjectOfType<AudioManager>().PlayFire();
    }
}
