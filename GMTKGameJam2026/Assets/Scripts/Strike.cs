using UnityEngine;
using UnityEngine.UI;

public class Strike : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private Image _icon;
    [SerializeField] private Material _redMat;
    private bool _striken;

    // =================== Function ===================
    #region Function
    public bool GetIsStriken()
    {
        return _striken;
    }

    public void SetStrike()
    {
        _striken = true;
        _icon.material = _redMat;
    }
    #endregion
}
