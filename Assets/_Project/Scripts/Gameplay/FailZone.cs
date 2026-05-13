using UnityEngine;

public class FailZone : MonoBehaviour
{
    private void Reset()
    {
        gameObject.tag = "FailZone";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
        }
    }
}
