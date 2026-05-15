using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private Transform levelRoot;
    [SerializeField] private LevelDatabase database;
    [SerializeField] private int startIndex;
    [SerializeField] private PickerController picker;
    [SerializeField] private Vector3 pickerSpawnPosition;

    private GameObject currentLevelInstance;
    private int currentIndex = -1;

    public LevelData CurrentLevelData { get; private set; }
    public int CurrentIndex => currentIndex;

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
        if (database != null && database.levels != null && database.levels.Length > 0)
        {
            LoadLevel(startIndex);
        }
    }

    public void LoadLevel(int index)
    {
        if (database == null || database.levels == null) return;
        if (index < 0 || index >= database.levels.Length) return;

        if (currentLevelInstance != null) Destroy(currentLevelInstance);

        currentIndex = index;
        CurrentLevelData = database.levels[index];

        if (CurrentLevelData != null && CurrentLevelData.levelPrefab != null)
        {
            currentLevelInstance = Instantiate(
                CurrentLevelData.levelPrefab,
                levelRoot != null ? levelRoot : transform);
        }

        if (picker != null && CurrentLevelData != null)
        {
            picker.MoveSpeed = CurrentLevelData.pickerSpeed;
            picker.transform.position = pickerSpawnPosition;
            picker.SyncTargetToCurrent();
        }

        if (LevelProgress.Instance != null && picker != null && CurrentLevelData != null)
        {
            LevelProgress.Instance.Configure(
                picker.transform,
                CurrentLevelData.progressStartZ,
                CurrentLevelData.progressEndZ);
        }
    }

    public void ReloadCurrent()
    {
        if (currentIndex >= 0) LoadLevel(currentIndex);
    }

    public void LoadNext()
    {
        if (database == null || database.levels == null || database.levels.Length == 0) return;
        int next = (currentIndex + 1) % database.levels.Length;
        LoadLevel(next);
    }
}
