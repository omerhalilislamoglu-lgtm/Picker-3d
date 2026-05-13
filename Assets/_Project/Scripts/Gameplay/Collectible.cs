using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Collectible : MonoBehaviour
{
    private bool hasBeenCounted;

    public bool TryCount()
    {
        if (hasBeenCounted) return false;
        hasBeenCounted = true;
        return true;
    }

    private void Reset()
    {
        gameObject.tag = "Collectible";
    }
}
