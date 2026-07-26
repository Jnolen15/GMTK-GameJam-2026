using UnityEngine;

public class Task : MonoBehaviour
{
    // ------------------------------------- Variables -------------------------------------
    [SerializeField] protected float  _taskTime; // time before the task overrides
    [SerializeField] protected int _taskFrequencyRate; // task rarity 1-3, 3 being common
    protected float  _taskTimeStamp; // time stamp used 
    protected bool _taskStarted = false;
    protected bool _taskPassed = true;


    // references
    [SerializeField] private TaskData _taskData;
    [SerializeField] private GameObject _rootUI;
    [SerializeField] private WindowControl _windowControl; // reference to the timer/window/popup whaterver created by this task
    public delegate void TaskEvent(Task obj);
    public static event TaskEvent OnTaskCreated;
    public static event TaskEvent OnTaskUpdate;
    public static event TaskEvent OnTaskFinished;
    public static event TaskEvent OnTaskFailed;


    // ------------------------------------- Functions -------------------------------------
    protected virtual void Start()
    {
        // subscribe to events

        // Starts when instantiated

        OnTaskCreated?.Invoke(this);
        StartTask();

        _windowControl.SetWindowName(_taskData.GetTaskName());
    }

    protected virtual void OnDestroy()
    {
        // unsubscribe from events
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Time.time > _taskTimeStamp)
        {
            // fail and clos task
            _taskPassed = true;
            CloseTask(_taskPassed);
        } else
        {
            // update timer
            float curTime = _taskTimeStamp - Time.time;
            _windowControl.UpdateTimer(curTime);
        }
    }

    public void ShowHint()
    {
        _windowControl.SetWindowHint(_taskData.GetTaskHint());
    }

    // declaration
    public Task (string name)
    {
        StartTask();
    }

    public virtual void StartTask()
    {
        Debug.Log("Created Task");
        _taskTimeStamp = Time.time + _taskTime;
    }


    public virtual void CloseTask(bool passed)
    {
        if (passed)
        {
            OnTaskFinished?.Invoke(this);
            StatTracker.Instance.IncrementTasksCompleted();
        } else
        {
            OnTaskFailed?.Invoke(this);
        }
        Destroy(this.gameObject);
    }


    // Helpers
    public GameObject GetTaskUIObject() { return _rootUI; }
    public WindowControl GetTaskUIWindowControl() { return _rootUI.GetComponent<WindowControl>(); }
    public TaskData GetTaskData() { return _taskData; }
    public float GetCurrentTaskTimer() { return _taskTimeStamp - Time.time; }

    public float GetFrequencyRate() { return _taskFrequencyRate; }
}
