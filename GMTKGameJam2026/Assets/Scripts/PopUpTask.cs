using JetBrains.Annotations;
using UnityEngine;

public class PopUpTask : Task
{
    [SerializeField] private int _overrideClicksToClose = 10;
    private int _clickCounter = 0;

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

    public override void CloseTask()
    {
        if (_override)
        {
            if (_clickCounter < _overrideClicksToClose)
            {
                _clickCounter += 1;
            } else
            {
                base.CloseTask();
            }
        } else
        {
            base.CloseTask();
        }
        
    }

    public override void StartOverride()
    {
        base.StartOverride();
    }
}
