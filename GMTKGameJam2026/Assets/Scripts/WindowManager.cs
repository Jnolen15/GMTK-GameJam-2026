using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private Transform _windowZone;
    [SerializeField] private Transform _taskElements;
    [SerializeField] private GameObject _taskTrayElementPref;
    [SerializeField] private TaskData _testNewTaskData;

    private List<TaskTrayElement> _trayElementList = new List<TaskTrayElement>();
    private Canvas _canvas;

    // =================== Setup ===================
    #region Setup
    private void Start()
    {
        _canvas = GetComponent<Canvas>();

        Task.OnTaskCreated += CreateTaskTrayElement;
        Task.OnTaskFinished += RemoveTaskTrayElement;
        Task.OnTaskFailed += RemoveTaskTrayElement;
    }

    private void OnDestroy()
    {
        Task.OnTaskCreated -= CreateTaskTrayElement;
        Task.OnTaskFinished -= RemoveTaskTrayElement;
        Task.OnTaskFailed -= RemoveTaskTrayElement;
    }
    #endregion

    // =================== Function ===================
    #region Function
    private void CreateTaskTrayElement(Task newTask)
    {
        TaskTrayElement tte = Instantiate(_taskTrayElementPref, _taskElements).GetComponent<TaskTrayElement>();
        tte.Setup(newTask);
        _trayElementList.Add(tte);

        WindowControl windowCont = newTask.GetTaskUIWindowControl();
        windowCont.SetTrayElement(tte);
    }

    private void RemoveTaskTrayElement(Task shutdownTask)
    {
        TaskTrayElement shutdownTaskElement = null;
        foreach (TaskTrayElement tte in _trayElementList)
        {
            if (tte.CompareAndShutdown(shutdownTask))
            {
                shutdownTaskElement = tte;
                break;
            }
        }

        if (shutdownTaskElement)
            Destroy(shutdownTaskElement.gameObject);
        else
            Debug.LogWarning("No associated task tray element found!");
    }

    public void MakeMeFavoriteChild(Transform targetTrans)
    {
        Debug.Log(targetTrans.name + " is my new fav!", targetTrans.gameObject);

        targetTrans.SetAsLastSibling();
    }
    #endregion
}
