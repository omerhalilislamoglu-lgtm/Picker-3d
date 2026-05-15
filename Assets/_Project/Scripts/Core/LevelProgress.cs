using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance { get; private set; }

    [SerializeField] private Transform tracked;
    [SerializeField] private float startZ;
    [SerializeField] private float endZ = 100f;

    public float Progress01 { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Configure(Transform target, float start, float end)
    {
        tracked = target;
        startZ = start;
        endZ = end;
    }

    private void Update()
    {
        if (tracked == null || endZ <= startZ)
        {
            Progress01 = 0f;
            return;
        }
        float t = (tracked.position.z - startZ) / (endZ - startZ);
        Progress01 = Mathf.Clamp01(t);
    }
}
