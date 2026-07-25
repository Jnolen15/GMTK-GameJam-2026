using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "SOs/TaskData")]
public class TaskData : ScriptableObject
{
    // =================== Data ===================
    [SerializeField] private string _taskName;
    [SerializeField] private Sprite _taskIcon;
    [SerializeField] private Material _taskIconMat;
    [SerializeField] private string _taskHint;

    // =================== Functions ===================
    public string GetTaskName() { return _taskName; }
    public Sprite GetTaskIcon() { return _taskIcon; }
    public Material GetTaskMaterial() { return _taskIconMat; }
    public string GetTaskHint() { return _taskHint; }
}
