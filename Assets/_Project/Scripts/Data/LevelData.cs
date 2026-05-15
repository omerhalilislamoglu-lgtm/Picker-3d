using UnityEngine;

[CreateAssetMenu(fileName = "Level_New", menuName = "Picker 3D/Level Data")]
public class LevelData : ScriptableObject
{
    public GameObject levelPrefab;
    public float pickerSpeed = 5f;
    public int[] gateThresholds;

    [Header("Star Thresholds (collected balls at truck)")]
    public int oneStarFill = 3;
    public int twoStarFill = 6;
    public int threeStarFill = 10;

    [Header("Rewards")]
    public int baseGoldReward = 50;
    public int perBallGold = 5;

    [Header("Progress Bar Z-range")]
    public float progressStartZ = 0f;
    public float progressEndZ = 100f;
}
