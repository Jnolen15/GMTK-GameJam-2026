using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HintPopup : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private TextMeshProUGUI _hintText;
    [SerializeField] private RectTransform _rectTrans;

    // =================== Function ===================
    #region Function
    public void Setup(string hintText)
    {
        _hintText.text = hintText;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTrans);
    }
    #endregion
}
