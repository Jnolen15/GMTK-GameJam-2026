using UnityEngine;

public class WindowManager : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private Transform _windowZone;
    [SerializeField] private Transform _taskElements;
    [SerializeField] private GameObject _taskTrayElementPref;
    [SerializeField] private GameObject _testNewTask;
    [SerializeField] private TaskData _testNewTaskData;
    private Canvas _canvas;

    // =================== Setup ===================
    #region Setup
    private void Start()
    {
        _canvas = GetComponent<Canvas>();

        // Subscribe to task creation method

        // TEST
        CreateTaskTrayElement(_testNewTask);
    }

    private void OnDestroy()
    {
        // Unsubscribe to task creation method
    }
    #endregion

    // =================== Function ===================
    #region Function
    private void CreateTaskTrayElement(GameObject newTask)
    {
        TaskTrayElement tte = Instantiate(_taskTrayElementPref, _taskElements).GetComponent<TaskTrayElement>();
        tte.Setup(newTask, _testNewTaskData);

        WindowControl windowCont = newTask.GetComponent<WindowControl>();
        windowCont.SetTrayElement(tte);
    }

    public void MakeMeFavoriteChild(RectTransform rectTrans)
    {
        rectTrans.SetAsLastSibling();
    }
    #endregion
}
