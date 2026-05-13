using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private int requiredCount = 5;
    [SerializeField] private TMP_Text label;
    [SerializeField] private Color passColor = new Color(0.2f, 1f, 0.2f);
    [SerializeField] private Color failColor = new Color(1f, 0.3f, 0.3f);
    [SerializeField] private Color pendingColor = Color.white;

    public int RequiredCount { get => requiredCount; set { requiredCount = value; UpdateLabel(); } }

    private int currentCount;
    private bool resolved;

    private void Start()
    {
        if (label != null) label.color = pendingColor;
        UpdateLabel();
    }

    private void Reset()
    {
        gameObject.tag = "Gate";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (resolved) return;

        if (other.CompareTag("Collectible"))
        {
            var c = other.GetComponentInParent<Collectible>();
            if (c != null && c.TryCount())
            {
                currentCount++;
                UpdateLabel();
            }
        }
        else if (other.CompareTag("Player"))
        {
            Resolve();
        }
    }

    private void Resolve()
    {
        resolved = true;
        bool pass = currentCount >= requiredCount;
        if (label != null) label.color = pass ? passColor : failColor;

        if (!pass)
        {
            GameManager.Instance?.Lose();
            Debug.Log($"Gate FAIL: {currentCount} / {requiredCount}");
        }
        else
        {
            Debug.Log($"Gate PASS: {currentCount} / {requiredCount}");
        }
    }

    private void UpdateLabel()
    {
        if (label != null) label.text = $"{currentCount} / {requiredCount}";
    }
}
