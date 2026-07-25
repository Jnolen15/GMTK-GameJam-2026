using UnityEngine;

public class StatTracker : MonoBehaviour
{
    // =================== Singelton ===================
    public static StatTracker Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // =================== Refrences ===================
    private int _sanctifications;
    private int _tasksCompleted;

    // =================== Function ===================
    #region Function
    public void IncrementSanctifications()
    {
        _sanctifications++;
    }

    public int GetNumSanctifications()
    {
        // -1 becuase it starts at 1
        return _sanctifications-1;
    }

    public void IncrementTasksCompleted()
    {
        _tasksCompleted++;
    }

    public int GetNumTasksCompleted()
    {
        return _tasksCompleted;
    }
    #endregion
}
