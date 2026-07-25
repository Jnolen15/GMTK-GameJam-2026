
using UnityEngine;

[CreateAssetMenu(fileName = "Person", menuName = "SOs/Person")]
public class PersonSO : ScriptableObject
{
    // =================== Data ===================
    [SerializeField] private string _name;
    [SerializeField] private int _age;
    [SerializeField] private string _gender;
    [SerializeField] private string _bloodType;
    [SerializeField] private bool _virginity;


    // =================== Functions ===================

    public PersonSO()
    {
        
    }

    // get
    public string GetName() { return _name; }
    public int GetAge() { return _age; }

    public string GetGender() { return _gender;  }

    public string GetBloodType() { return _bloodType; }

    public bool GetVirginity() { return _virginity;  }

    // set
    public void SetName(string s) { _name = s; }
    public void SetAge(int i) { _age = i; }

    public void SetGender(string s) { _gender = s; }

    public void SetBloodType(string s) { _bloodType = s; }

    public void SetVirginity(bool b) { _virginity = b; }


}

