using UnityEngine;

public class EndRamp : MonoBehaviour
{
    [SerializeField] private float boost = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (boost <= 0f) return;
        if (other.CompareTag("Collectible") && other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddForce(Vector3.forward * boost, ForceMode.Impulse);
        }
    }
}
