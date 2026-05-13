using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite filledSprite;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Color filledColor = new Color(1f, 0.85f, 0.2f);
    [SerializeField] private Color emptyColor = new Color(1f, 1f, 1f, 0.25f);

    public void SetStars(int count)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] == null) continue;
            bool filled = i < count;
            if (filled && filledSprite != null) stars[i].sprite = filledSprite;
            else if (!filled && emptySprite != null) stars[i].sprite = emptySprite;
            stars[i].color = filled ? filledColor : emptyColor;
        }
    }
}
