using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using TMPro;

public class Task : MonoBehaviour
{
    // ------------------------------------- Variables -------------------------------------
    [SerializeField] private string  _taskName; // name of the task
    [SerializeField] private float  _taskTime; // time before the task overrides
    protected float  _taskTimeStamp; // time stamp used 
    protected bool _taskStarted = false;
    protected private bool _override = false;

    [SerializeField] private TextMeshProUGUI _TimerReference; // reference to the timer/window/popup whaterver created by this task

    public  delegate void TaskEvent(string i);
    public static event TaskEvent OnTaskUpdate;
    public static event TaskEvent OnTaskFinished;
    public static event TaskEvent OnTaskOvertime;


    // ------------------------------------- Functions -------------------------------------
    void Start()
    {
        // subscribe to events

        // Starts when instantiated
        StartTask();

    }

    public void OnDestroy()
    {
        // unsubscribe from events
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _taskTimeStamp)
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
        _taskName = name;
        Debug.Log("Created Task");
        _taskTimeStamp = Time.time + _taskTime;
    }

    public virtual void CloseTask()
    {
        OnTaskFinished?.Invoke(_taskName);
        Destroy(this);
    }

    public virtual void StartOverride()
    {
        OnTaskOvertime?.Invoke(_taskName);
        _override = true;
    }


}
