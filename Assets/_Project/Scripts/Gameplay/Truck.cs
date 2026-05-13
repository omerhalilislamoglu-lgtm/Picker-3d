using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] private float resolveDelay = 1.5f;

    public int CollectedCount { get; private set; }

    private bool resolved;
    private float resolveTimer;
    private bool collecting;

    private void Reset()
    {
        gameObject.tag = "EndZone";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (resolved) return;
        if (other.CompareTag("Collectible"))
        {
            CollectedCount++;
            resolveTimer = resolveDelay;
            collecting = true;
        }
    }

    private void Update()
    {
        if (resolved || !collecting) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.State != GameManager.GameState.Playing) return;

        resolveTimer -= Time.deltaTime;
        if (resolveTimer <= 0f)
        {
            ResolveWin();
        }
    }

    private void ResolveWin()
    {
        resolved = true;
        int stars = CalculateStars();
        GameManager.Instance?.Win(stars);
        Debug.Log($"Truck: {CollectedCount} collected → {stars}★");
    }

    private int CalculateStars()
    {
        var data = LevelManager.Instance?.CurrentLevelData;
        if (data == null) return CollectedCount > 0 ? 1 : 0;

        if (CollectedCount >= data.threeStarFill) return 3;
        if (CollectedCount >= data.twoStarFill) return 2;
        if (CollectedCount >= data.oneStarFill) return 1;
        return 0;
    }
}
