using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskTrayElement : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private TextMeshProUGUI _taskName;
    [SerializeField] private Image _taskImage;
    private GameObject _taskUIRoot;
    private bool _taskOpen;

    // =================== Setup ===================
    #region Setup
    public void Setup(Task task)
    {
        _taskOpen = true;
        _taskUIRoot = task.GetTaskUIObject();

        TaskData data = task.GetTaskData();
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
        _taskUIRoot.SetActive(_taskOpen);
    }
    #endregion
}
