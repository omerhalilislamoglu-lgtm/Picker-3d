using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 axis = Vector3.up;
    [SerializeField] private float degreesPerSecond = 120f;
    [SerializeField] private Space rotationSpace = Space.Self;

    private void Update()
    {
        transform.Rotate(axis, degreesPerSecond * Time.deltaTime, rotationSpace);
    }
}
