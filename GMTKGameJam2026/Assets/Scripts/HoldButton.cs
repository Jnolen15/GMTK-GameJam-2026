using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _holdTimeRequirement;
    private float _holdTimer;
    private bool _buttonHeld;

    public UnityEvent _completedEvent;

    // =================== Function ===================
    #region Function
    private void Update()
    {
        if (_buttonHeld)
        {
            if (_holdTimer < _holdTimeRequirement)
                _holdTimer += Time.deltaTime;
            else
                FinishHold();
        }

        _progressBar.fillAmount = _holdTimer / _holdTimeRequirement;
    }

    public void OnPointerDown()
    {
        _buttonHeld = true;
    }

    public void OnPointerUp()
    {
        _buttonHeld = false;
        _holdTimer = 0;
    }

    private void FinishHold()
    {
        _completedEvent?.Invoke();

        _buttonHeld = false;
        _holdTimer = 0;
    }
    #endregion
}
