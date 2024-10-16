using System;
using Unity.VisualScripting;
using UnityEngine;

public class teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination1;
    [SerializeField] private Transform destination2;
    [SerializeField] private Transform destination3;
    readonly string[] teleporterPuzzleNames = {"Main", "A1", "A2", "B1", "B2", "C1", "C2", "D1", "D2"};

    private int index = 1;

    public Transform GetDestination()
    {
        string teleporterName = gameObject.name;
        if (Array.Exists(teleporterPuzzleNames, element => element == teleporterName)){
            GameObject torchManager = GameObject.Find("TorchManager");
            if (torchManager != null)
            { 
                Transform dest = TMGetDestination(torchManager);
                return dest;
            }
        }

        
        GameObject secretSwitchObject = GameObject.Find("SecretSwitch");        
        if (secretSwitchObject != null)
        {
            return SSGetDestination(secretSwitchObject);
        }

        return destination1;
    }

    private Transform SSGetDestination(GameObject secretSwitchObject){
        if (secretSwitchObject.TryGetComponent<SecretSwitch>(out var secretSwitch))
        {
            index = secretSwitch.index;
            return index switch
            {
                1 => destination1,
                2 => destination2,
                _ => destination3
            };
        }
        return destination1;
    }

    private Transform TMGetDestination(GameObject torchManager){
        if (torchManager.TryGetComponent<TorchManager>(out var torchManagerComponent))
        {
            int state = torchManagerComponent.state;
            // Get the name of this teleporter
            string teleporterName = gameObject.name;
            // Switch on the teleporter name
            if (teleporterName == "Main"){
                return state switch
                {
                    _ when state == 2 || state == 4 || state == 7 => destination1,
                    _ when state == 5 || state == 6 => destination2,
                    _ when state == 1 || state == 3 => destination3,
                    _ => null
                };
                
            }
            return teleporterName switch
            {
                "A1" => (state == 1 || state == 2 || state == 7) ? destination1 : (state == 4) ? destination2 : null,
                "A2" => (state == 2 || state == 4 || state == 7) ? destination1 : (state == 5) ? destination2 : (state == 3) ? destination3 : null,
                "B1" => (state == 6) ? destination1 : (state == 5 || state == 3) ? destination2 : (state == 7) ? destination3 : null,
                "B2" => (state == 5 || state == 6) ? destination1 : (state == 4) ? destination2 : null,
                "C1" => (state == 5) ? destination1 : null,
                "C2" => null,
                "D1" => (state == 1 || state == 3) ? destination1 : (state == 4 || state == 1) ? destination2 : null,
                "D2" => (state == 2 || state == 1) ? destination1 : (state == 7 || state == 3) ? destination2 : null,
                _ => null
            };
        }        
        return destination1;
    }

}