using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "People", menuName = "SOs/People")]
public class PeopleSO : ScriptableObject
{
    // =================== Data ===================
    [SerializeField] private List<string> _names = new List<string>();
    public int _maxAge = 100;
    public int _minAge = 18;
    [SerializeField] private List<string> _genders = new List<string>();
    [SerializeField] private List<string> _bloodTypes = new List<string>();




    // =================== Functions ===================
    public string GetName() { Debug.Log(_names.Count); return _names[Random.Range(0, _names.Count)]; }
    public int GetAge() { return Random.Range(_minAge, _maxAge); }

    public string GetGender() { return _genders[Random.Range(0, _genders.Count)]; }

    public string GetBloodType() { return _bloodTypes[Random.Range(0, _bloodTypes.Count)]; }

    public bool GetVirginity() { return Random.Range(0, 2) == 0 ? true : false; }
}
