using UnityEngine;

public class WindowManager : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private Transform _windowZone;
    private Canvas _canvas;

    // =================== Setup ===================
    #region Setup
    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }
    #endregion

    // =================== Function ===================
    #region Function
    public void MakeMeFavoriteChild(RectTransform rectTrans)
    {
        rectTrans.SetAsLastSibling();
    }
    #endregion
}
