using UnityEngine;
using UnityEngine.EventSystems;

public class WindowControl : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    // =================== Refrences ===================
    private Canvas _canvas;
    private Camera _cam;
    private RectTransform _rectTransform;
    private WindowManager _winMan;
    private TaskTrayElement _trayElement;
    private bool _moveable;

    // =================== Setup ===================
    #region Setup
    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _cam = _canvas.worldCamera;
        _rectTransform = GetComponent<RectTransform>();
        _winMan = GetComponentInParent<WindowManager>();
    }

    public void SetTrayElement(TaskTrayElement trayElement)
    {
        _trayElement = trayElement;
    }
    #endregion

    // =================== Function ===================
    #region Function
    public void ToggleWindow()
    {
        _trayElement.ToggleTask();
    }
    #endregion

    // =================== Interface ===================
    #region Interface
    public void OnDrag(PointerEventData eventData)
    {
        if(_moveable)
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, _cam, out Vector2 localPoint))
        {
            _winMan.MakeMeFavoriteChild(_rectTransform);

            float halfHeight = (_rectTransform.rect.height / 2);
            float topZone = (halfHeight - 50);

            if (localPoint.y > topZone)
                _moveable = true;
            else
                _moveable = false;
        }
    }
    #endregion
}
