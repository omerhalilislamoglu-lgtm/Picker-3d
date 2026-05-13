using UnityEngine;

[CreateAssetMenu(fileName = "Level_New", menuName = "Picker 3D/Level Data")]
public class LevelData : ScriptableObject
{
    public GameObject levelPrefab;
    public float pickerSpeed = 5f;
    public int[] gateThresholds;
    public int oneStarFill = 3;
    public int twoStarFill = 6;
    public int threeStarFill = 10;
}
