using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskTrayElement : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private TextMeshProUGUI _taskName;
    [SerializeField] private Image _taskImage;
    private GameObject _taskObj;
    private bool _taskOpen;

    // =================== Setup ===================
    #region Setup
    public void Setup(GameObject taskObj, TaskData data)
    {
        _taskOpen = true;
        _taskObj = taskObj;

        _taskName.text = data.GetTaskName();
        _taskImage.sprite = data.GetTaskIcon();
        _taskImage.material = data.GetTaskMaterial();
    }
    #endregion

    // =================== Function ===================
    #region Function
    public void ToggleTask()
    {
        _taskOpen = !_taskOpen;
        _taskObj.SetActive(_taskOpen);
    }
    #endregion
}
