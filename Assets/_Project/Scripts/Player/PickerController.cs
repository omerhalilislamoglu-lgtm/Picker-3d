using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float halfTrackWidth = 2.5f;
    [SerializeField] private float lateralLerp = 18f;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private Rigidbody rb;
    private float targetX;
    private bool initialized;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    private void OnEnable()
    {
        SyncTargetToCurrent();
    }

    public void SyncTargetToCurrent()
    {
        targetX = transform.position.x;
        initialized = true;
    }

    private void Update()
    {
        if (InputManager.Instance == null) return;
        targetX = Mathf.Clamp(targetX + InputManager.Instance.HorizontalDelta, -halfTrackWidth, halfTrackWidth);
    }

    private void FixedUpdate()
    {
        if (!initialized) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.State != GameManager.GameState.Playing) return;

        Vector3 pos = rb.position;
        pos.x = Mathf.Lerp(pos.x, targetX, 1f - Mathf.Exp(-lateralLerp * Time.fixedDeltaTime));
        pos.z += moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }
}
