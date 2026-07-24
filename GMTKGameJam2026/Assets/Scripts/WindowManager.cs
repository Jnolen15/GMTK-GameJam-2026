using UnityEngine;

public class WindowManager : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private Transform _windowZone;
    [SerializeField] private Transform _taskElements;
    [SerializeField] private GameObject _taskTrayElementPref;
    [SerializeField] private TaskData _testNewTaskData;
    private Canvas _canvas;

    // =================== Setup ===================
    #region Setup
    private void Start()
    {
        _canvas = GetComponent<Canvas>();

        Task.OnTaskCreated += CreateTaskTrayElement;
    }

    private void OnDestroy()
    {
        Task.OnTaskCreated -= CreateTaskTrayElement;
    }
    #endregion

    // =================== Function ===================
    #region Function
    private void CreateTaskTrayElement(Task newTask)
    {
        TaskTrayElement tte = Instantiate(_taskTrayElementPref, _taskElements).GetComponent<TaskTrayElement>();
        tte.Setup(newTask);

        WindowControl windowCont = newTask.GetTaskUIWindowControl();
        windowCont.SetTrayElement(tte);
    }

    public void MakeMeFavoriteChild(Transform targetTrans)
    {
        Debug.Log(targetTrans.name + " is my new fav!", targetTrans.gameObject);

        targetTrans.SetAsLastSibling();
    }
    #endregion
}
