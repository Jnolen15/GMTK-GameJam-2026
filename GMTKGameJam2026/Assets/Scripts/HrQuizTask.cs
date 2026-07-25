using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class HrQuizTask : Task
{
    // ------------------------------------- Variables -------------------------------------

    [Header("HrQuizTask")]
    private int _questionIndex = 0;
    private int _questionCount = 5;

    // references 
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;


    // replaced by the stuff in the scriptable object later
    [SerializeField] private List<String> _questions = new List<String>();
    [SerializeField] private List<String> _rightAnswers = new List<String>();
    [SerializeField] private List<String> _wrongAnswers = new List<String>();


    // ------------------------------------- Functions -------------------------------------
    public HrQuizTask(string input) : base(input)
    {

    }

    protected override void Start()
    {
        // subscribe to stuff
        base.Start();

        // taking question count
        _questionCount = _questions.Count;

        LoadNextQuestion();
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

        // set question text
        _questionText.text = _questions[_questionIndex];

        // Randomly assing the right/wrong asnwers to one button or the other
        if (UnityEngine.Random.Range(1, 3) == 1)
        {
            // assigning answer text
            ConfigureButton(_leftButton, _rightAnswers[_questionIndex], true);
            ConfigureButton(_rightButton, _wrongAnswers[_questionIndex], false);
        }
        else {
            ConfigureButton(_leftButton, _wrongAnswers[_questionIndex], false);
            ConfigureButton(_rightButton, _rightAnswers[_questionIndex], true);
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
    }

    public void WrongAnswer()
    {
        // it sez u suck or smthn
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

