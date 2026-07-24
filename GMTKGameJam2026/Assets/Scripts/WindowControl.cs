using UnityEngine;
using UnityEngine.EventSystems;

public class WindowControl : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    // =================== Refrences ===================
    private Canvas _canvas;
    private Camera _cam;
    private RectTransform _rectTransform;
    private WindowManager _winMan;
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
    #endregion

    // =================== Function ===================
    #region Function
    private void Update()
    {
        
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
            Debug.Log($"Mouse Position inside UI: {localPoint} {halfHeight} {topZone}");

            if (localPoint.y > topZone)
                _moveable = true;
            else
                _moveable = false;
        }
    }
    #endregion
}
