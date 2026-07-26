using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.MemoryProfiler;

public class PasswordTask : Task
{
    // ------------------------------------- Variables -------------------------------------

    // references
    [SerializeField] TMP_InputField _textInput;
    [SerializeField] private TextMeshProUGUI _accountText;

    private PasswordSo _passwordSO;



    // references 

    [SerializeField] private List<PasswordSo> _passwordSOs = new List<PasswordSo>();

    // ------------------------------------- Functions -------------------------------------
    public PasswordTask(string input) : base(input)
    {

    }

    protected override void Start()
    {
        // subscribe to stuff
        base.Start();
        

        // adding listener to inputfield
        _textInput.onValueChanged.AddListener(delegate { PasswordCheck(); });

        // select random password

        _passwordSO = _passwordSOs[Random.Range(0, _passwordSOs.Count)];

        // configure text 
        _accountText.text = _passwordSO.GetAccount();
    }

    public void PasswordCheck() {
        if (_textInput.text == _passwordSO.GetPassword())
        {
            CloseTask(true);
        }
    }

    protected override void OnDestroy()
    {
        // unsubscribe from stuff
        base.OnDestroy();
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
