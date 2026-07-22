using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class CamPositioner : MonoBehaviour
{
    // =================== Refrences ===================
    [Header("Tween")]
    [SerializeField] private float _travelTime;

    [Header("Positions")]
    [SerializeField] private Transform _camPosPC;
    [SerializeField] private Transform _camPosRight;
    [SerializeField] private Transform _camPosLeft;
    private Camera _cam;
    private CamPosition _currentPos;
    private float _camCDTime;

    private enum CamPosition
    {
        Center,
        Left,
        Right,
    }


    // =================== Setup ===================
    #region Setup
    private void Start()
    {
        _cam = Camera.main;
    }
    #endregion

    // =================== Function ===================
    #region Function
    private void Update()
    {
        if (_camCDTime >= 0)
            _camCDTime -= Time.deltaTime;
        else
        {
            Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

            // if look left
            if (viewportPoint.x < 0.1f)
            {
                if (_currentPos == CamPosition.Center)
                    Look(CamPosition.Left);
                if (_currentPos == CamPosition.Right)
                    Look(CamPosition.Center);
            }
            // if look right
            else if (viewportPoint.x > 0.9f)
            {
                if (_currentPos == CamPosition.Center)
                    Look(CamPosition.Right);
                if (_currentPos == CamPosition.Left)
                    Look(CamPosition.Center);
            }
        }
    }

    private void Look(CamPosition newPos)
    {
        _camCDTime = 0.5f;
        _currentPos = newPos;

        switch (_currentPos)
        {
            case CamPosition.Center:
                MoveCam(_camPosPC);
                break;
            case CamPosition.Left:
                MoveCam(_camPosLeft);
                break;
            case CamPosition.Right:
                MoveCam(_camPosRight);
                break;
            default:
                break;
        }
    }

    public void MoveCam(Transform camPos)
    {
        _cam.transform.DOMove(camPos.position, _travelTime).SetEase(Ease.InOutSine); 
        _cam.transform.DORotate(camPos.rotation.eulerAngles, _travelTime).SetEase(Ease.InOutSine);
    }
    #endregion
}
