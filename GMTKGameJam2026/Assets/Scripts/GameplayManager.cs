using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEditor.Rendering;

public class GameplayManager : MonoBehaviour
{
    [Header("Check for fast testing")]
    [SerializeField] private bool UsingTestParameters = false;

    // ------------------------------------- Variables -------------------------------------
    [Header("Variables")]
    [SerializeField] public float _gameStartTime = 0; // keeps track of when the game "starts"
    
    [SerializeField] private float _shiftLengthMinutes = 30; // longest possible time the game can run
    [SerializeField] private float _baseTaskDeviationSeconds = 15; // time between giving the player tasks
    [SerializeField] private float _possibleTaskDeviationSeconds = 2.5f;
    [SerializeField] private float _taskDeviationScaler = 0.9f; // multiplier that reduces time between each subsequent task
    [SerializeField] private float _taskDeviationFloor = 5f; // minimum time between tasks
    private float _totalTaskWieght = 0;

    private float _maxStrikes = 5;
    private float _curStrikes;

    // main task variables
    private Boolean _mainTaskStarted = false;
    private float _mainTaskTimeStamp; // stamp for starting the main task
    [SerializeField] private float _mainTaskTimeSeconds = 60; // Time until the main task ends
    private float _mainTaskDelayTimeStamp; // stamp for when the main task stats counting down again
    [SerializeField] private float _mainTaskDelaySeconds = 15; // Time the main task delays until coninuing to count down
    private float _mainTaskSecondsLeft; // keeps track of the main task time left
    private bool _mainTaskTimerRunStart;


    private Boolean _gameOver = false;

    
    // lists
    private List<GameObject> _taskList = new List<GameObject>();

    // references
    public List<TaskSpawnEntry> _introTasks = new List<TaskSpawnEntry>();
    private List<GameObject> _taskReferences = new List<GameObject>();
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Transform _windowZone;
    private int _introTaskIndex;

    // events
    public delegate void GameManagerEvent(float input);
    public static event GameManagerEvent OnGameTimerStart;
    public static event GameManagerEvent OnMainTimerUpdate;
    public static event GameManagerEvent OnMainTaskDelayReset;
    public static event GameManagerEvent OnMainTaskTimerStart;
    public static event GameManagerEvent OnGameOver;

    public delegate void GameEndEvent();
    public static event GameEndEvent OnGameEndWin;
    public static event GameEndEvent OnGameEndSanctifyLoss;
    public static event GameEndEvent OnGameEndTaskLoss;

    [System.Serializable]
    public class TaskSpawnEntry
    {
        public float _spawnTime;
        public GameObject _taskPref;
    }


    // ------------------------------------- Functions -------------------------------------
    #region Functions
    void Start()
    {
        Task.OnTaskFailed += ReceiveFailedTask;

        if (UsingTestParameters) UseTestParameters();

        _gameStartTime = Time.time;
        OnGameTimerStart?.Invoke(_gameStartTime);

        StartCoroutine(TaskTimer(_baseTaskDeviationSeconds));

        StartMainTask();
    }

    private void OnDestroy()
    {
        Task.OnTaskFailed -= ReceiveFailedTask;
    }

    // tweaked testing values to check if they are working faster
    private void UseTestParameters()
    {
        _shiftLengthMinutes = 0.5f;
        _mainTaskDelaySeconds = 2;
        _mainTaskTimeSeconds = 15;
        _baseTaskDeviationSeconds = 4;
        _possibleTaskDeviationSeconds = 1;
        _taskDeviationFloor = 1;
    }

    void Update()
    {
        // Intro Tasks
        if(_introTaskIndex < _introTasks.Count)
        {
            if (_introTasks[_introTaskIndex]._spawnTime < GetAdjustedGameTime())
                SpawnIntroTask();
        }

        // Run main task timer
        if (_mainTaskStarted && GetAdjustedGameTime() > _mainTaskDelayTimeStamp)
        {
            _mainTaskSecondsLeft -= Time.deltaTime;
            OnMainTimerUpdate?.Invoke(_mainTaskSecondsLeft);

            if (!_mainTaskTimerRunStart)
                MainTaskTimerStarted();
        }

        // end game checks
        if (!_gameOver && _mainTaskStarted)
        {
            if (GetAdjustedGameTime() > _shiftLengthMinutes * 60f) EndGame(1);
            if (_mainTaskSecondsLeft <= 0f) EndGame(2);
        }
    }

    public float GetAdjustedGameTime()
    {
        return Time.time - _gameStartTime;
    }

    private void MainTaskTimerStarted()
    {
        _mainTaskTimerRunStart = true;
        OnMainTaskTimerStart?.Invoke(_mainTaskDelayTimeStamp);
    }

    public void ResetMainTaskDelay()
    {
        _mainTaskDelayTimeStamp = GetAdjustedGameTime() + _mainTaskDelaySeconds;
        _mainTaskTimerRunStart = false;

        OnMainTaskDelayReset?.Invoke(_mainTaskDelayTimeStamp);

        StatTracker.Instance.IncrementSanctifications();
    }

    public void StartMainTask()
    {
        _mainTaskStarted = true;
        ResetMainTaskDelay(); // start delay
        _mainTaskSecondsLeft = _mainTaskTimeSeconds; // set the dynamic var

        OnMainTimerUpdate?.Invoke(_mainTaskTimeSeconds); // initialize UI element text

        Debug.Log("Main task Started");
    }

    public void SpawnIntroTask()
    {
        Debug.Log("Spawn " + _introTaskIndex);

        // Spawn new task
        GameObject newTask = Instantiate(_introTasks[_introTaskIndex]._taskPref, _windowZone);

        // display hint
        newTask.GetComponent<Task>().ShowHint();

        // move it to a random place on the screen
        newTask.GetComponent<RectTransform>().anchoredPosition = GetRandomScreenPos();

        // add task to list
        _taskList.Add(newTask);

        // Add task to spawn pool
        _taskReferences.Add(_introTasks[_introTaskIndex]._taskPref);

        // add task weight to spawn pool
        _totalTaskWieght += _introTasks[_introTaskIndex]._taskPref.GetComponent<Task>().GetFrequencyRate();

        // Increment intro index
        _introTaskIndex++;
    }

    public void StartTask(int index)
    {
        // create new task
        GameObject newTask = Instantiate(_taskReferences[index], _windowZone);

        // move it to a random place on the screen
        newTask.GetComponent<RectTransform>().anchoredPosition = GetRandomScreenPos();
         
        // add task to list
        _taskList.Add(newTask);
    }

    public void EndGame(int type)
    {
        _gameOver = true;
        OnGameOver?.Invoke(0);
        Debug.Log("Game OVER");

        if (type == 1)
            OnGameEndWin?.Invoke();
        else if (type == 2)
            OnGameEndSanctifyLoss?.Invoke();
        else if (type == 3)
            OnGameEndTaskLoss?.Invoke();
    }

    private void ReceiveFailedTask(Task task)
    {
        _curStrikes++;

        if (_curStrikes >= _maxStrikes)
            EndGame(3);
    }

    private Vector2 GetRandomScreenPos()
    {
        RectTransform rt = _windowZone.GetComponent<RectTransform>();
        float x = rt.rect.width / 4;
        float y = -rt.rect.height / 6;
        return new Vector2(UnityEngine.Random.Range(-x, x), UnityEngine.Random.Range(-y, y));
    }

    #endregion

    // ------------------------------------- Coroutines -------------------------------------
    private IEnumerator TaskTimer(float time)
    {
        // wait the alloted time to start thetask
        yield return new WaitForSeconds(time);

        if(!_gameOver)
        {
            // start a random task
            float weightNum = UnityEngine.Random.Range(0, _totalTaskWieght); // roll the target weight
            float taskWeightIterator = 0; // adds weights until its more than num
            int taskIndex = 0;
            taskWeightIterator += _taskReferences[taskIndex].GetComponent<Task>().GetFrequencyRate();
            Debug.Log(weightNum + "/" + _totalTaskWieght);

            while (taskWeightIterator < weightNum)
            {
                taskIndex++;
                taskWeightIterator += _taskReferences[taskIndex].GetComponent<Task>().GetFrequencyRate();
                Debug.Log(taskWeightIterator + "/" + weightNum);
                Debug.Log(taskIndex + " out of " + _taskReferences.Count + " tasks");
                
            }

            StartTask(taskIndex);



            // loop and start the next task with less time, floors at _minSecondsBetweenTasks
            
            float taskDelayOffset = UnityEngine.Random.Range(-_possibleTaskDeviationSeconds, _possibleTaskDeviationSeconds);
            float taskDelay = (time) * _taskDeviationScaler + taskDelayOffset;
            Debug.Log(taskDelay + " seconds delay");
            if (!_gameOver) StartCoroutine(TaskTimer(_taskDeviationFloor > taskDelay  ? _taskDeviationFloor : taskDelay));
        }
    }

}
