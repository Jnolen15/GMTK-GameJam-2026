using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "SOs/HRQuizQ")]
public class HRQuizQSO : ScriptableObject
{
    // =================== Data ===================
    [TextArea]
    [SerializeField] private string _question;
    [TextArea]
    [SerializeField] private string _answerRight;
    [TextArea]
    [SerializeField] private string _answerWrong;

    // =================== Functions ===================
    public string GetQuizQuestion() { return _question; }
    public string GetQuizRightAnswer() { return _answerRight; }
    public string GetQuizWrongAnswer() { return _answerWrong; }
}
