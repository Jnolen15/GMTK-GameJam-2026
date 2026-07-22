using System.Threading;
using UnityEngine;

public class gameplayManager : MonoBehaviour
{
    // ------------------------------------- Variables -------------------------------------
    [Header("Variables")]
    [SerializeField] public float gameStartTime = 0;
    [SerializeField] private float shiftLengthMinutes = 30;

    [HeaderAttribute("timers")]
    [SerializeField] private float shiftTimeStamp;
    [SerializeField] private float buttonTimeStamp;
    [SerializeField] private float coworkerTimeStamp;
    [SerializeField] private float hrTimeStamp;
    [SerializeField] private float wifiGlitchTimeStamp;
    [SerializeField] private float wifiDownTimeStamp;
    [SerializeField] private float printerJamTimeStamp;
    [SerializeField] private float pritnerFireTimeStamp;

    void Start()
    {
        gameStartTime = Time.time;
    }

    void Update()
    {
        
    }



    // current tasks
    // main task - game over
    // coworker - HR course
    // wifi - wifi falire
    // printer - printer faliure

    public void StartTask(string task)
    {
        
    }
}
