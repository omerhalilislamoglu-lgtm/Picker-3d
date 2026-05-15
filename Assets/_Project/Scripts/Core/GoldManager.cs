using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    private const string PrefKey = "picker3d.gold";

    public static GoldManager Instance { get; private set; }

    public int Gold { get; private set; }
    public event Action<int> OnGoldChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Gold = PlayerPrefs.GetInt(PrefKey, 0);
    }

    private void Start()
    {
        OnGoldChanged?.Invoke(Gold);
    }

    public void Add(int amount)
    {
        if (amount <= 0) return;
        Gold += amount;
        PlayerPrefs.SetInt(PrefKey, Gold);
        PlayerPrefs.Save();
        OnGoldChanged?.Invoke(Gold);
    }

    public bool TrySpend(int amount)
    {
        if (amount <= 0 || Gold < amount) return false;
        Gold -= amount;
        PlayerPrefs.SetInt(PrefKey, Gold);
        PlayerPrefs.Save();
        OnGoldChanged?.Invoke(Gold);
        return true;
    }
}
