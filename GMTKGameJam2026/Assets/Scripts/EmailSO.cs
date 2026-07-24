using UnityEngine;

[CreateAssetMenu(fileName = "EmailSO", menuName = "SOs/Email")]
public class EmailSO : ScriptableObject
{
    // =================== Data ===================
    [SerializeField] private string _emailSender;
    [TextArea]
    [SerializeField] private string _emailBodyText;

    // =================== Functions ===================
    public string GetEmailSender() { return _emailSender; }
    public string GetEmailBody() { return _emailBodyText; }
}
