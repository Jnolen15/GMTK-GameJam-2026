using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopUpTask : Task
{
    // ------------------------------------- Variables -------------------------------------

    // References
    public List<Button> ButtonList = new List<Button>();


    // ------------------------------------- Functions -------------------------------------
    public PopUpTask(string input) : base(input)
    {

    }
    
    protected override void Start()
    {
        // subscribe to stuff
        base.Start();
        
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
