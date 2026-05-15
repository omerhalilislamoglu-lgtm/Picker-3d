using UnityEngine;

public class MobileBootstrap : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60;
    [SerializeField] private bool keepScreenAwake = true;

    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0;
        if (keepScreenAwake) Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
