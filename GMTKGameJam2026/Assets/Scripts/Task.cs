using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Task : MonoBehaviour
{
    // ------------------------------------- Variables -------------------------------------
    [SerializeField] protected float  _taskTime; // time before the task overrides
    protected float  _taskTimeStamp; // time stamp used 
    protected bool _taskStarted = false;
    protected private bool _override = false;


    // references
    [SerializeField] private TaskData _taskData;
    [SerializeField] private GameObject _rootUI;
    [SerializeField] private TextMeshProUGUI _TimerReference; // reference to the timer/window/popup whaterver created by this task
    public delegate void TaskEvent(Task obj);
    public static event TaskEvent OnTaskCreated;
    public static event TaskEvent OnTaskUpdate;
    public static event TaskEvent OnTaskFinished;
    public static event TaskEvent OnTaskOvertime;


    // ------------------------------------- Functions -------------------------------------
    protected virtual void Start()
    {
        // subscribe to events

        // Starts when instantiated

        OnTaskCreated?.Invoke(this);
        StartTask();

    }

    protected virtual void OnDestroy()
    {
        // unsubscribe from events
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!_override && Time.time > _taskTimeStamp)
        {
            StartOverride();
        } else
        {
            _TimerReference.text = (_taskTimeStamp - Time.time).ToString("#.00");
        }
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

    public virtual void CloseTask()
    {
        OnTaskFinished?.Invoke(this);
        Destroy(this.gameObject);
    }

    public virtual void StartOverride()
    {
        OnTaskOvertime?.Invoke(this);
        _override = true;
    }

    // Helpers
    public GameObject GetTaskUIObject() { return _rootUI; }
    public WindowControl GetTaskUIWindowControl() { return _rootUI.GetComponent<WindowControl>(); }
    public TaskData GetTaskData() { return _taskData; }
}
