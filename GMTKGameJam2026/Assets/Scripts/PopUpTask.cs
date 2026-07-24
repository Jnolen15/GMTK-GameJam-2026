using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PopUpTask : Task
{
    // ------------------------------------- Variables -------------------------------------

    // references 
    [SerializeField] private GameObject _fakeButtons;

    // ------------------------------------- Functions -------------------------------------
    public PopUpTask(string input) : base(input)
    {

    }
    
    protected override void Start()
    {
        // subscribe to stuff
        base.Start();
        List<Button> ButtonList = _fakeButtons.GetComponentsInChildren<Button>().ToList<Button>();

        // pick random button to be "real"
        ButtonList[Random.Range(0, ButtonList.Count - 1)].onClick.AddListener(delegate { CloseTask(true); });
    }

    protected override void OnDestroy()
    {
       // unsubscribe from stuff
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
