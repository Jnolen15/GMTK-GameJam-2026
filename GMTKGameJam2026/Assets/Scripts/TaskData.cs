using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "SOs/TaskData")]
public class TaskData : ScriptableObject
{
    // =================== Data ===================
    [SerializeField] private string _taskName;
    [SerializeField] private Sprite _taskIcon;
    [SerializeField] private Material _taskIconMat;

    // =================== Functions ===================
    public string GetTaskName() { return _taskName; }
    public Sprite GetTaskIcon() { return _taskIcon; }
    public Material GetTaskMaterial() { return _taskIconMat; }
}
