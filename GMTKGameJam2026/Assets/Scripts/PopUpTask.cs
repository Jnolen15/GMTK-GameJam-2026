using UnityEngine;

public class PopUpTask : Task
{

    public PopUpTask(string input) : base(input)
    {

    }
    
    void Start()
    {
        // subscribe to stuff
    }

    private void OnDestroy()
    {
       // unsubscribe from stuff
    }


    void Update()
    {
        
    }


}
