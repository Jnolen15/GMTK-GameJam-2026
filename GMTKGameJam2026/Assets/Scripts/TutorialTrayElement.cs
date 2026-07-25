using UnityEngine;

public class TutorialTrayElement : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private GameObject _taskUIRoot;
    private bool _taskOpen = true;

    // =================== Function ===================
    #region Function
    public void ToggleTask()
    {
        _taskOpen = !_taskOpen;
        _taskUIRoot.SetActive(_taskOpen);
    }
    #endregion
}
