using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class WindowControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler
{
    // =================== Refrences ===================
    [SerializeField] private Transform _rootObj;
    [SerializeField] private TextMeshProUGUI _windowName;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private HintPopup _hint;

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

    public void SetWindowName(string winName)
    {
        _windowName.text = winName;
    }
    
    public void SetWindowHint(string hintText)
    {
        _hint.gameObject.SetActive(true);
        _hint.Setup(hintText);
    }
    #endregion

    // =================== Function ===================
    #region Function
    public void ToggleWindow()
    {
        _trayElement.ToggleTask();
    }

    public void UpdateTimer(float curTime)
    {
        int minutes = Mathf.FloorToInt(curTime / 60);
        int seconds = Mathf.FloorToInt(curTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (curTime < 10)
            _timerText.color = Color.red;
        else if (curTime < 30)
            _timerText.color = Color.orange;
        else if (curTime < 60)
            _timerText.color = Color.yellow;
    }
    #endregion

    // =================== Interface ===================
    #region Interface
    public void OnDrag(PointerEventData eventData)
    {
        if (_moveable)
        {
            Vector3 mousePos = _cam.ScreenToViewportPoint(eventData.position);

            if(mousePos.x > 0 && mousePos.x < 1 && mousePos.y > 0.1 && mousePos.y < 1)
                _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, _cam, out Vector2 localPoint))
        {
            float halfHeight = (_rectTransform.rect.height / 2);
            float topZone = (halfHeight - 50);

            if (localPoint.y > topZone)
                _moveable = true;
            else
                _moveable = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _winMan.MakeMeFavoriteChild(_rootObj);
    }
    #endregion
}
