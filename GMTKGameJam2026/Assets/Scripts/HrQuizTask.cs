using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HrQuizTask : Task
{
    // ------------------------------------- Variables -------------------------------------

    private int _questionIndex;
    private int _questionCount;

    // references 
    [Header("HrQuizTask")]
    [SerializeField] private int _quizLength;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private List<HRQuizQSO> _questions = new List<HRQuizQSO>();
    [SerializeField] private GameObject _incorrectWarning;
    [SerializeField] private TextMeshProUGUI _requirementText;

    private List<HRQuizQSO> _curQuestions = new List<HRQuizQSO>();


    // ------------------------------------- Functions -------------------------------------
    public HrQuizTask(string input) : base(input)
    {

    }

    protected override void Start()
    {
        // subscribe to stuff
        base.Start();

        // make quiz
        _curQuestions = new List<HRQuizQSO>();
        List<HRQuizQSO> tempList = new List<HRQuizQSO>();
        tempList.AddRange(_questions);
        for (int i = 0; i < _quizLength; i++)
        {
            int randPick = Random.Range(0, tempList.Count);
            _curQuestions.Add(tempList[randPick]);
            tempList.RemoveAt(randPick);
        }
        _questionCount = _curQuestions.Count;

        LoadNextQuestion();

        UpdateProgress();
    }

    protected override void OnDestroy()
    {
        // unsubscribe from stuff
        base.OnDestroy();
    }

    protected override void Update()
    {
        // need to update timer here
        base.Update();
    }

    // opens the next question of the quiz
    public void LoadNextQuestion()
    {
        // exit if possible
        if (_questionIndex >= _questionCount)
        {
            _taskPassed = true;
            CloseTask(true);
            return;
        }

        // Get question data
        HRQuizQSO questionData = _curQuestions[_questionIndex];

        // set question text
        _questionText.text = questionData.GetQuizQuestion();

        // Randomly assing the right/wrong asnwers to one button or the other
        if (UnityEngine.Random.Range(1, 3) == 1)
        {
            // assigning answer text
            ConfigureButton(_leftButton, questionData.GetQuizRightAnswer(), true);
            ConfigureButton(_rightButton, questionData.GetQuizWrongAnswer(), false);
        }
        else {
            ConfigureButton(_leftButton, questionData.GetQuizWrongAnswer(), false);
            ConfigureButton(_rightButton, questionData.GetQuizRightAnswer(), true);
        }

        // Increment Question Index;
        _questionIndex += 1;
    }

    private void ConfigureButton(Button button, string text, bool isRight)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        button.onClick.RemoveAllListeners();
        if (isRight)
        {
            button.onClick.AddListener(delegate { RightAnswer(); });
        } else
        {
            button.onClick.AddListener(delegate { WrongAnswer(); });
        }
    }

    public void RightAnswer()
    {
        LoadNextQuestion();

        UpdateProgress();
        _incorrectWarning.SetActive(false);
    }

    public void WrongAnswer()
    {
        _incorrectWarning.SetActive(true);
    }

    private void UpdateProgress()
    {
        _requirementText.text = $"{_questionIndex-1} / {_curQuestions.Count}";
    }

    public override void StartTask()
    {
        base.StartTask();
    }

    public override void CloseTask(bool passed)
    {
        // task can't close unless complete
        base.CloseTask(passed);

    }
}

