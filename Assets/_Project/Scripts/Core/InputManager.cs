using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private float lateralRange = 5f;
    [SerializeField] private float deadZonePixels = 2f;

    public float HorizontalDelta { get; private set; }
    public bool IsPointerDown { get; private set; }

    private Vector2 lastPointerPos;
    private bool wasDown;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        HorizontalDelta = 0f;

        var pointer = Pointer.current;
        if (pointer == null)
        {
            IsPointerDown = false;
            wasDown = false;
            return;
        }

        bool isDown = false;
        if (Mouse.current != null) isDown = Mouse.current.leftButton.isPressed;
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed) isDown = true;

        IsPointerDown = isDown;
        Vector2 current = pointer.position.ReadValue();

        if (isDown && !wasDown)
        {
            lastPointerPos = current;
        }
        else if (isDown)
        {
            float rawDx = current.x - lastPointerPos.x;
            if (Mathf.Abs(rawDx) >= deadZonePixels)
            {
                float screenW = Mathf.Max(1f, Screen.width);
                HorizontalDelta = (rawDx / screenW) * lateralRange;
                lastPointerPos = current;
            }
        }

        wasDown = isDown;
    }
}
