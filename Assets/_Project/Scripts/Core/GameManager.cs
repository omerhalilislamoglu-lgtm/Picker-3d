using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public enum GameState { Idle, Playing, Win, Lose }

    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Idle;
    public int CurrentStars { get; private set; }

    public event Action OnGameStart;
    public event Action<int> OnGameWin;
    public event Action OnGameLose;
    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetState(GameState.Idle);
        Debug.Log("GameManager: Idle");
    }

    private void Update()
    {
        if (State != GameState.Idle) return;
        var kb = Keyboard.current;
        bool keyStart = kb != null && kb.sKey.wasPressedThisFrame;
        bool tapStart = Pointer.current != null && Pointer.current.press.wasPressedThisFrame;
        if (keyStart || tapStart) StartGame();
    }

    public void StartGame()
    {
        if (State != GameState.Idle && State != GameState.Lose && State != GameState.Win) return;
        SetState(GameState.Playing);
        OnGameStart?.Invoke();
        Debug.Log("GameManager: Playing");
    }

    public void Win(int stars)
    {
        if (State != GameState.Playing) return;
        CurrentStars = Mathf.Clamp(stars, 0, 3);
        SetState(GameState.Win);
        OnGameWin?.Invoke(CurrentStars);
        Debug.Log($"GameManager: Win ({CurrentStars}★)");
    }

    public void Lose()
    {
        if (State != GameState.Playing) return;
        SetState(GameState.Lose);
        OnGameLose?.Invoke();
        Debug.Log("GameManager: Lose");
    }

    public void ResetToIdle()
    {
        SetState(GameState.Idle);
        CurrentStars = 0;
    }

    private void SetState(GameState next)
    {
        State = next;
        OnStateChanged?.Invoke(next);
    }
}
