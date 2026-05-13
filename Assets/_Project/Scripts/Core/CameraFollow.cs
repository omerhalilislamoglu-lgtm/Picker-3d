using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -7f);
    [SerializeField] private float followLerp = 8f;
    [SerializeField] private bool followX = false;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;
        if (!followX) desired.x = transform.position.x;

        transform.position = Vector3.Lerp(transform.position, desired, followLerp * Time.deltaTime);
    }

    public void SetTarget(Transform t) => target = t;
}
