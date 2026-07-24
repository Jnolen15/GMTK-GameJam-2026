using JetBrains.Annotations;
using UnityEngine;

public class PopUpTask : Task
{
    [SerializeField] private float _taskTime = 15;

    public PopUpTask(string input) : base(input)
    {

    }
    
    protected override void Start()
    {
        // subscribe to stuff
    }

    protected override void OnDestroy()
    {
       // unsubscribe from stuff
    }


    protected override void Update()
    {
        
    }

    public override void StartTask()
    {
        base.StartTask();
    }

    public override void CloseTask()
    {
        base.CloseTask();
    }

    public override void StartOverride()
    {
        base.StartOverride();
    }
}
