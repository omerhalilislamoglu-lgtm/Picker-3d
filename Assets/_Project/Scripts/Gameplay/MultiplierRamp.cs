using UnityEngine;

public class MultiplierRamp : MonoBehaviour
{
    [SerializeField] private int multiplier = 2;
    [SerializeField] private float forwardBoost = 0f;

    public int Multiplier => multiplier;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collectible")) return;

        var c = other.GetComponentInParent<Collectible>();
        if (c != null) c.SetMultiplier(multiplier);

        if (forwardBoost > 0f && other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddForce(Vector3.forward * forwardBoost, ForceMode.Impulse);
        }
    }
}
