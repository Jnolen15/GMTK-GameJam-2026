using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class AnswerEmailTask : Task
{
    // ------------------------------------- Variables -------------------------------------

    [SerializeField] private int _minCharacterCount;

    // references 
    [SerializeField] private TextMeshProUGUI _textInput;
    [SerializeField] private Button _sendButton;

    // ------------------------------------- Functions -------------------------------------
    public AnswerEmailTask(string input) : base(input)
    {

    }

    protected override void Start()
    {
        // subscribe to stuff
        base.Start();

        // adding send attempt event to button
        _sendButton.onClick.AddListener(delegate { SendEmail(); });
    }

    protected override void OnDestroy()
    {
        // unsubscribe from stuff
        base.OnDestroy();
    }

    private void SendEmail() { 
        if (_textInput.text.Length >= _minCharacterCount)
        {
            CloseTask(_taskPassed);
        } else
        {
            Debug.Log("Not enough characters to send email");
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void StartTask()
    {
        base.StartTask();
    }

    public override void CloseTask(bool passed)
    {
        base.CloseTask(passed);

    }
}
