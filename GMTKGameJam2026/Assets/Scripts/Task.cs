using UnityEngine;

public class Task : MonoBehaviour
{
    // ------------------------------------- Variables -------------------------------------
    [SerializeField] private string taskName;

    [SerializeField] private float taskTimeStamp;


    // ------------------------------------- Functions -------------------------------------
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

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
    
  

}
