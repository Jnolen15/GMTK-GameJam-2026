using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskTrayElement : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private TextMeshProUGUI _taskName;
    [SerializeField] private Image _taskImage;
    [SerializeField] private Image _warningIcon;
    [SerializeField] private Material _yellowMat;
    [SerializeField] private Material _orangeMat;
    [SerializeField] private Material _redMat;
    private Task _linkedTask;
    private GameObject _taskUIRoot;
    private bool _taskOpen;

    // =================== Setup ===================
    #region Setup
    public void Setup(Task task)
    {
        _linkedTask = task;
        _taskOpen = true;
        _taskUIRoot = task.GetTaskUIObject();

        TaskData data = task.GetTaskData();
        _taskName.text = data.GetTaskTrayName();
        _taskImage.sprite = data.GetTaskIcon();
        _taskImage.material = data.GetTaskMaterial();
    }
    #endregion

    // =================== Function ===================
    #region Function
    private void Update()
    {
        if(_linkedTask)
            UpdateWarningIcon(_linkedTask.GetCurrentTaskTimer());
    }

    private void UpdateWarningIcon(float taskTime)
    {
        if (taskTime < 60)
            _warningIcon.gameObject.SetActive(true);
        else
            _warningIcon.gameObject.SetActive(false);

        if (taskTime < 10)
        {
            _warningIcon.material = _redMat;
            _warningIcon.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        }
        else if (taskTime < 30)
        {
            _warningIcon.material = _orangeMat;
            _warningIcon.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (taskTime < 60)
        {
            _warningIcon.material = _yellowMat;
            _warningIcon.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        }
    }

    public void ToggleTask()
    {
        _taskOpen = !_taskOpen;
        _taskUIRoot.SetActive(_taskOpen);
    }

    public bool CompareAndShutdown(Task task)
    {
        if (_linkedTask == task)
            return true;
        else
            return false;
    }
    #endregion
}
