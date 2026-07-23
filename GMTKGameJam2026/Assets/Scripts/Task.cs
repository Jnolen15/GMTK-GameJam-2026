using UnityEngine;

public class Task : MonoBehaviour
{
    // ------------------------------------- Variables -------------------------------------
    [SerializeField] private string taskName;

    [SerializeField] private float taskTimeStamp;

    public  delegate void TaskEvent(float i);
    public static event TaskEvent OnTaskUpdate;


    // ------------------------------------- Functions -------------------------------------
    void Start()
    {
        // subscribe to events

    }

    // Update is called once per frame
    void Update()
    {
        // unsubscribe from events
    }



    // declaration
    public Task (string name)
    {
        InitializeTask(name);
    }

    public void InitializeTask(string name)
    {
        taskName = name;
        Debug.Log("Created Task");
        taskTimeStamp = Time.time;
    }

    public void OnDestroy()
    {
        // unsubscribe from events
    }

}
