using UnityEngine;

public class basicTeleporter : MonoBehaviour
{
    private int index = 1;
    [SerializeField] private Transform destination;

    public Transform GetDestination()
    {
        string teleporterName = gameObject.name;
        return destination;
    }
}
