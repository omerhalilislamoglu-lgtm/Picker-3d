using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Collectible : MonoBehaviour
{
    private bool hasBeenCounted;

    public int Multiplier { get; private set; } = 1;

    public bool TryCount()
    {
        if (hasBeenCounted) return false;
        hasBeenCounted = true;
        return true;
    }

    public void SetMultiplier(int value)
    {
        if (value > Multiplier) Multiplier = value;
    }

    private void Reset()
    {
        gameObject.tag = "Collectible";
    }
}
