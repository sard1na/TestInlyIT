using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform background;
    public RectTransform handle;

    public Vector2 InputDirection { get; private set; }

    private Vector2 center;

    void Start()
    {
        center = background.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position;
        Vector2 direction = pos - center;
        float maxRadius = background.sizeDelta.x / 2f;

        InputDirection = Vector2.ClampMagnitude(direction, maxRadius) / maxRadius;
        handle.position = center + InputDirection * maxRadius;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector2.zero;
        handle.position = center;
    }
}
