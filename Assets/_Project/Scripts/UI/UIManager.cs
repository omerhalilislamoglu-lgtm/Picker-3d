using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private StarDisplay winStars;
    [SerializeField] private TMP_Text levelLabel;
    [SerializeField] private Button tapToStartButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextButton;

    private GameManager subscribed;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            Subscribe(GameManager.Instance);
        }

        if (tapToStartButton != null) tapToStartButton.onClick.AddListener(OnTapToStart);
        if (retryButton != null) retryButton.onClick.AddListener(OnRetry);
        if (nextButton != null) nextButton.onClick.AddListener(OnNext);

        ShowOnly(mainMenuPanel);
    }

    private void OnDestroy()
    {
        if (subscribed != null) Unsubscribe(subscribed);
    }

    private void Subscribe(GameManager gm)
    {
        gm.OnGameStart += HandleGameStart;
        gm.OnGameWin += HandleGameWin;
        gm.OnGameLose += HandleGameLose;
        subscribed = gm;
    }

    private void Unsubscribe(GameManager gm)
    {
        gm.OnGameStart -= HandleGameStart;
        gm.OnGameWin -= HandleGameWin;
        gm.OnGameLose -= HandleGameLose;
    }

    private void HandleGameStart()
    {
        ShowOnly(hudPanel);
        if (levelLabel != null) levelLabel.text = "Level";
    }

    private void HandleGameWin(int stars)
    {
        ShowOnly(winPanel);
        if (winStars != null) winStars.SetStars(stars);
    }

    private void HandleGameLose() => ShowOnly(losePanel);

    private void OnTapToStart() => GameManager.Instance?.StartGame();

    private void OnRetry()
    {
        GameManager.Instance?.ResetToIdle();
        LevelManager.Instance?.ReloadCurrent();
        ShowOnly(mainMenuPanel);
    }

    private void OnNext()
    {
        GameManager.Instance?.ResetToIdle();
        LevelManager.Instance?.LoadNext();
        ShowOnly(mainMenuPanel);
    }

    private void ShowOnly(GameObject panel)
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(panel == mainMenuPanel);
        if (hudPanel != null) hudPanel.SetActive(panel == hudPanel);
        if (winPanel != null) winPanel.SetActive(panel == winPanel);
        if (losePanel != null) losePanel.SetActive(panel == losePanel);
    }
}
