using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class PasswordsTXT : MonoBehaviour
{
    [SerializeField] private List<PasswordSo> _passwords = new List<PasswordSo>();

    void Start()
    {
        foreach (PasswordSo p in _passwords)
        {
            p.GeneratePassword();
        }


        // acquire and set textboxes
        List<TextMeshProUGUI> _textBoxes = this.GetComponentsInChildren<TextMeshProUGUI>().ToList();

        foreach (TextMeshProUGUI t in _textBoxes)
        {
            PasswordSo temp = _passwords[Random.Range(0, _passwords.Count)];
            _passwords.Remove(temp);
            t.text = temp.GetAccount() + ": " + temp.GetPassword();
        }
    }
}
