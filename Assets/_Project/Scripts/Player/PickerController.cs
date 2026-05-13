using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float halfTrackWidth = 2.5f;
    [SerializeField] private float lateralMultiplier = 1f;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private Rigidbody rb;
    private float pendingLateral;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        if (InputManager.Instance == null) return;
        pendingLateral += InputManager.Instance.HorizontalDelta * lateralMultiplier;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.State != GameManager.GameState.Playing)
        {
            pendingLateral = 0f;
            return;
        }

        Vector3 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x + pendingLateral, -halfTrackWidth, halfTrackWidth);
        pos.z += moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
        pendingLateral = 0f;
    }
}
