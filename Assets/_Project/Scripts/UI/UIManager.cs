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
    [SerializeField] private TMP_Text goldLabel;
    [SerializeField] private TMP_Text rewardLabel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Button tapToStartButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextButton;

    private GameManager subscribedGm;
    private GoldManager subscribedGold;
    private int goldBeforeWin;

    private void Start()
    {
        if (GameManager.Instance != null) SubscribeGm(GameManager.Instance);
        if (GoldManager.Instance != null) SubscribeGold(GoldManager.Instance);

        if (tapToStartButton != null) tapToStartButton.onClick.AddListener(OnTapToStart);
        if (retryButton != null) retryButton.onClick.AddListener(OnRetry);
        if (nextButton != null) nextButton.onClick.AddListener(OnNext);

        UpdateLevelLabel();
        ShowOnly(mainMenuPanel);
    }

    private void OnDestroy()
    {
        if (subscribedGm != null) UnsubscribeGm(subscribedGm);
        if (subscribedGold != null) subscribedGold.OnGoldChanged -= HandleGoldChanged;
    }

    private void Update()
    {
        if (progressBar != null && LevelProgress.Instance != null)
        {
            progressBar.value = LevelProgress.Instance.Progress01;
        }
    }

    private void SubscribeGm(GameManager gm)
    {
        gm.OnGameStart += HandleGameStart;
        gm.OnGameWin += HandleGameWin;
        gm.OnGameLose += HandleGameLose;
        subscribedGm = gm;
    }

    private void UnsubscribeGm(GameManager gm)
    {
        gm.OnGameStart -= HandleGameStart;
        gm.OnGameWin -= HandleGameWin;
        gm.OnGameLose -= HandleGameLose;
    }

    private void SubscribeGold(GoldManager gm)
    {
        gm.OnGoldChanged += HandleGoldChanged;
        subscribedGold = gm;
        HandleGoldChanged(gm.Gold);
    }

    private void HandleGameStart()
    {
        goldBeforeWin = GoldManager.Instance != null ? GoldManager.Instance.Gold : 0;
        UpdateLevelLabel();
        ShowOnly(hudPanel);
    }

    private void HandleGameWin(int stars)
    {
        ShowOnly(winPanel);
        if (winStars != null) winStars.SetStars(stars);
        if (rewardLabel != null && GoldManager.Instance != null)
        {
            int delta = GoldManager.Instance.Gold - goldBeforeWin;
            rewardLabel.text = $"+{delta}";
        }
    }

    private void HandleGameLose() => ShowOnly(losePanel);

    private void HandleGoldChanged(int gold)
    {
        if (goldLabel != null) goldLabel.text = gold.ToString();
    }

    private void OnTapToStart() => GameManager.Instance?.StartGame();

    private void OnRetry()
    {
        GameManager.Instance?.ResetToIdle();
        LevelManager.Instance?.ReloadCurrent();
        UpdateLevelLabel();
        ShowOnly(mainMenuPanel);
    }

    private void OnNext()
    {
        GameManager.Instance?.ResetToIdle();
        LevelManager.Instance?.LoadNext();
        UpdateLevelLabel();
        ShowOnly(mainMenuPanel);
    }

    private void UpdateLevelLabel()
    {
        if (levelLabel == null) return;
        int idx = LevelManager.Instance != null ? LevelManager.Instance.CurrentIndex : 0;
        levelLabel.text = $"Level {idx + 1}";
    }

    private void ShowOnly(GameObject panel)
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(panel == mainMenuPanel);
        if (hudPanel != null) hudPanel.SetActive(panel == hudPanel);
        if (winPanel != null) winPanel.SetActive(panel == winPanel);
        if (losePanel != null) losePanel.SetActive(panel == losePanel);
    }
}
