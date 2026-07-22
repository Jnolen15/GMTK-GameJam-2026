using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using System;

public class GameplayManager : MonoBehaviour
{
    // ------------------------------------- Variables -------------------------------------
    [Header("Variables")]
    [SerializeField] public float gameStartTime = 0; // keeps track of when the game "starts"
    


    [SerializeField] private float _shiftLengthMinutes = 30; // longest possible time the game can run
    [SerializeField] private float _taskDeviationSeconds = 15; // time between giving the player tasks
    [SerializeField] private float _taskDeviationScaler = 0.9f; // multiplier that reduces time between each subsequent task
    [SerializeField] private float _minSecondsBetweenTask = 5; // minimum time between tasks

    // main task variables
    private Boolean mainTaskStarted = false;
    private float _mainTaskTimeStamp; // stamp for starting the main task
    [SerializeField] private float _mainTaskTimeSeconds = 60; // Time until the main task ends
    private float _mainTaskDelayTimeStamp; // stamp for when the main task stats counting down again
    [SerializeField] private float _mainTaskDelaySeconds = 15; // Time the main task delays until coninuing to count down

    
    // lists
    private List<Task> taskList = new List<Task>();


    // ------------------------------------- Functions -------------------------------------
    void Start()
    {
        gameStartTime = Time.time;
        StartCoroutine(TaskTimer(_taskDeviationSeconds));
    }

    void Update()
    {
        if (mainTaskStarted)    


        // end game check
        if (Time.time > _shiftLengthMinutes * 60) EndGame(); 
    }


    public void ResetMainTaskDelay()
    {
        ;
    }





    public void StartTask(string name)
    {
        // create new task

        // attatch it to this gameobject
        Task tempTask = this.gameObject.AddComponent<Task>();
        tempTask.InitializeTask(name);

        // add task to list
        taskList.Add(tempTask);
    }


    public void StartTask()
    {
        return;
    }

    public void EndGame()
    {
        Debug.Log("Game Over - shift finished");
    }

    // ------------------------------------- Coroutines -------------------------------------
    private IEnumerator TaskTimer(float time)
    {
        // wait the alloted time to start thetask
        yield return new WaitForSeconds(time);

        // start the task
        StartTask("Template Task");

        // loop and start the next task with less time, floors at _minSecondsBetweenTasks
        StartCoroutine(TaskTimer(time * _taskDeviationScaler > _minSecondsBetweenTask ? time * _taskDeviationScaler : _minSecondsBetweenTask));
    }

}
