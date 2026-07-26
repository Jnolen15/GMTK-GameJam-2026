using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "PasswordSo", menuName = "SOs/Password")]
public class PasswordSo : ScriptableObject
{
    // =================== Data ===================
    [SerializeField] private string _account;
    [SerializeField] private string _description;
    [SerializeField] private int _passwordLength;

    private string _password = null;


    private string _possibleChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWCYZ123456789!@#$%^&*?";




    // =================== Functions ===================

    public string GetAccount() { return _account; }

    public string GetDescription() { return _description; }

    public string GetPassword() {

        Debug.Log(_passwordLength);
        // generates password if its null
        if (_password == "")
        {
            string s = string.Empty;
            for (int i = 0; i < _passwordLength; i++)
            {
                s += _possibleChars[Random.Range(0, _possibleChars.Length)];
            }
            _password = s;
        }
       return _password;
    }
}
