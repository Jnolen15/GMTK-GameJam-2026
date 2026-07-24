using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainTaskTimerUI : MonoBehaviour
{
    // =================== Variables ===================
    [SerializeField] private TextMeshProUGUI _textBox;
    [SerializeField] private CanvasGroup _holdButtonCanvasGroup;

    // =================== Setup ===================
    #region Setup
    void Start()
    {
        GameplayManager.OnMainTimerUpdate += UpdateText;
        GameplayManager.OnMainTaskTimerStart += MainTimerResume;
        GameplayManager.OnMainTaskDelayReset += MainTimerPause;
    }

    private void OnDestroy()
    {
        GameplayManager.OnMainTimerUpdate -= UpdateText;
        GameplayManager.OnMainTaskTimerStart -= MainTimerResume;
        GameplayManager.OnMainTaskDelayReset -= MainTimerPause;
    }
    #endregion

    // =================== Function ===================
    #region Function
    private void MainTimerResume(float timeStamp)
    {
        ToggleTaskActive(true);
    }

    private void MainTimerPause(float timeStamp)
    {
        ToggleTaskActive(false);   
    }

    private void ToggleTaskActive(bool active)
    {
        _holdButtonCanvasGroup.interactable = active;
        _holdButtonCanvasGroup.blocksRaycasts = active;

        if (active)
            _holdButtonCanvasGroup.alpha = 1;
        else
            _holdButtonCanvasGroup.alpha = 0.5f;
    }

    private void UpdateText(float curTime)
    {
        int minutes = Mathf.FloorToInt(curTime / 60);
        int seconds = Mathf.FloorToInt(curTime % 60);
        _textBox.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (curTime < 10)
            _textBox.color = Color.red;
        else if (curTime < 30)
            _textBox.color = Color.orange;
        else if (curTime < 60)
            _textBox.color = Color.yellow;
    }
    #endregion
}
