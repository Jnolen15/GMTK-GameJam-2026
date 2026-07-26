using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "PasswordSo", menuName = "SOs/Password")]
public class PasswordSo : ScriptableObject
{
    // =================== Data ===================
    [SerializeField] private string _account;
    [SerializeField] private string _description;
    [SerializeField] private int _minPasswordLength;
    [SerializeField] private int _maxPasswordLength;
    private int _passwordLength;

    private string _password = null;


    [SerializeField] private string _possibleChars = "abcdefghjkmnopqrstuvwxyzABCDEFGHJKMNOPQRSTUVWCYZ123456789!@#$%^&*?";




    // =================== Functions ===================

    public string GetAccount() { return _account; }

    public string GetDescription() { return _description; }

    public void GeneratePassword()
    {
        // Debug.Log(_passwordLength);
        // generates password if its null
        string s = string.Empty;
        _passwordLength = Random.Range(_minPasswordLength, _maxPasswordLength + 1);
        for (int i = 0; i < _passwordLength; i++)
        {
            s += _possibleChars[Random.Range(0, _possibleChars.Length)];
        }
        _password = s;
    }

    public string GetPassword() {
       return _password;
    }
}
