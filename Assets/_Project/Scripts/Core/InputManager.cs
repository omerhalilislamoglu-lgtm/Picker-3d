using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private float sensitivity = 0.02f;

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
        if (pointer == null) return;

        bool isDown = false;

        if (Mouse.current != null)
            isDown = Mouse.current.leftButton.isPressed;
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            isDown = true;

        IsPointerDown = isDown;
        Vector2 current = pointer.position.ReadValue();

        if (isDown && !wasDown)
        {
            lastPointerPos = current;
        }
        else if (isDown)
        {
            float dx = (current.x - lastPointerPos.x) * sensitivity;
            HorizontalDelta = dx;
            lastPointerPos = current;
        }

        wasDown = isDown;
    }
}
