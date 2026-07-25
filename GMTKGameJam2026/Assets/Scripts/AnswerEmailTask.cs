using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class AnswerEmailTask : Task
{
    // ------------------------------------- Variables -------------------------------------

    [Header("Email Task")]
    [SerializeField] private Vector2 _minCharacterCountRange;
    private int _minCharacterCount;

    // references 
    [SerializeField] private List<EmailSO> _emails = new List<EmailSO>();
    [SerializeField] private TextMeshProUGUI _emailSender;
    [SerializeField] private TextMeshProUGUI _emailBody;
    [SerializeField] private TextMeshProUGUI _textInput;
    [SerializeField] private Button _sendButton;
    [SerializeField] private TextMeshProUGUI _requirementText;
    [SerializeField] private GameObject _notLongEnoughMessage;

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

        EmailSO emailData = _emails[Random.Range(0, _emails.Count)];
        _emailSender.text = "From: " + emailData.GetEmailSender();
        _emailBody.text = emailData.GetEmailBody();

        _minCharacterCount = (Random.Range((int)_minCharacterCountRange.x, (int)_minCharacterCountRange.y) * 10);
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
            _notLongEnoughMessage.SetActive(true);
            Debug.Log("Not enough characters to send email");
        }
    }

    protected override void Update()
    {
        _requirementText.text = $"{_textInput.text.Length} / {_minCharacterCount}";

        if (_textInput.text.Length < _minCharacterCount)
            _requirementText.color = Color.red;
        else
            _requirementText.color = Color.green;

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
