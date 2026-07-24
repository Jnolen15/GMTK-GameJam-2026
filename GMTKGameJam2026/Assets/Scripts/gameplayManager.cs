using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using System;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;

public class GameplayManager : MonoBehaviour
{
    [Header("Check for fast testing")]
    [SerializeField] private bool UsingTestParameters = false;

    // ------------------------------------- Variables -------------------------------------
    [Header("Variables")]
    [SerializeField] public float gameStartTime = 0; // keeps track of when the game "starts"
    
    [SerializeField] private float _shiftLengthMinutes = 30; // longest possible time the game can run
    [SerializeField] private float _taskDeviationSeconds = 15; // time between giving the player tasks
    [SerializeField] private float _taskDeviationScaler = 0.9f; // multiplier that reduces time between each subsequent task
    [SerializeField] private float _taskDeviationFloor = 5; // minimum time between tasks

    // main task variables
    private Boolean mainTaskStarted = false;
    private float _mainTaskTimeStamp; // stamp for starting the main task
    [SerializeField] private float _mainTaskTimeSeconds = 60; // Time until the main task ends
    private float _mainTaskDelayTimeStamp; // stamp for when the main task stats counting down again
    [SerializeField] private float _mainTaskDelaySeconds = 15; // Time the main task delays until coninuing to count down
    private float _mainTaskSecondsLeft; // keeps track of the main task time left


    private Boolean _gameOver = false;

    
    // lists
    private List<GameObject> _taskList = new List<GameObject>();

    // references
    public List<GameObject> _taskReferences = new List<GameObject>();
    [SerializeField] private Canvas _mainCanvas;

    // events
    public delegate void GameManagerEvent(float input);
    public static event GameManagerEvent OnMainTimerUpdate;
    public static event GameManagerEvent OnMainTaskDelayReset;
    public static event GameManagerEvent OnGameOver;




    // ------------------------------------- Functions -------------------------------------

    #region Functions
    void Start()
    {
        // subscribe to events

        if (UsingTestParameters) UseTestParameters();

        gameStartTime = Time.time;
        StartCoroutine(TaskTimer(_taskDeviationSeconds));

        StartMainTask();
    }

    private void OnDestroy()
    {
         // unsubscribe from events
    }

    // tweaked testing values to check if they are working faster
    private void UseTestParameters()
    {
        _shiftLengthMinutes = 0.5f;
        _mainTaskDelaySeconds = 2;
        _mainTaskTimeSeconds = 5;
        _taskDeviationSeconds = 3;
        _taskDeviationFloor = 1;
    }

    void Update()
    {
        if (mainTaskStarted && Time.time > _mainTaskDelayTimeStamp)
        {
            _mainTaskSecondsLeft -= Time.deltaTime;
            OnMainTimerUpdate?.Invoke(_mainTaskSecondsLeft);
        }

        // end game checks
        if (!_gameOver && mainTaskStarted)
        {
            if (Time.time > _shiftLengthMinutes * 60f + Time.time) EndGame("Game Over - shift finished with time to spare");
            if (_mainTaskSecondsLeft <= 0f) EndGame("Game Over - your countdown finished");
        }
        
    }


    public void ResetMainTaskDelay()
    {
        _mainTaskDelayTimeStamp = Time.time + _mainTaskDelaySeconds;
    }

    public void StartMainTask()
    {
        mainTaskStarted = true;
        ResetMainTaskDelay(); // start delay
        _mainTaskSecondsLeft = _mainTaskTimeSeconds; // set the dynamic var

        OnMainTimerUpdate?.Invoke(_mainTaskTimeSeconds); // initialize UI element text

        Debug.Log("Main task Started");
    }


    public void StartTask(int index)
    {
        // create new task
        GameObject newTask = Instantiate(_taskReferences[0], _mainCanvas.transform);

        // add task to list
        _taskList.Add(newTask);
    }

    public void EndGame(string text)
    {
        _gameOver = true;
        OnGameOver?.Invoke(0);
        Debug.Log(text);
    }

    #endregion

    // ------------------------------------- Coroutines -------------------------------------
    private IEnumerator TaskTimer(float time)
    {
        // wait the alloted time to start thetask
        yield return new WaitForSeconds(time);

        if(!_gameOver)
        {
            // start the task
            StartTask(0);

            // loop and start the next task with less time, floors at _minSecondsBetweenTasks
            if (!_gameOver) StartCoroutine(TaskTimer(time * _taskDeviationScaler > _taskDeviationFloor ? time * _taskDeviationScaler : _taskDeviationFloor));
        }
    }

}
