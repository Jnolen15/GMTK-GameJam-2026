using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.Analytics;

public class CensusTask : Task
{
    // ------------------------------------- Variables -------------------------------------

    private Exclusion _exclusionType;
    private PersonSO _excludedPerson;

    private List<PersonSO> _usedpeople = new List<PersonSO>();
    private List<Toggle> _toggleList;
    private int _peopleInCensus;

    // references 
    [SerializeField] private PeopleSO _peopleSO; 
    [SerializeField] private Button _submitButton;
    [SerializeField] private TextMeshProUGUI _exclusionTextBox;
    [SerializeField] private GameObject _faliureWarning;
    
    public enum Exclusion
    {
        Gender,
        BloodType,
        Age,
        Virginity
    }

    // ------------------------------------- Functions -------------------------------------
    public CensusTask(string input) : base(input)
    {
        _usedpeople = new List<PersonSO>();
    }

    protected override void Start()
    {
        // subscribe to stuff



        // Instantiate list
        _toggleList = new List<Toggle>();
        _usedpeople = new List<PersonSO>();

        // get all toggles
        _toggleList = this.GetComponentsInChildren<Toggle>().ToList();

        _peopleInCensus = _toggleList.Count;

        // add onclick event to button
        _submitButton.onClick.AddListener(delegate { Submit(); });


        GenerateCensus();


        base.Start();
    }

    public void Submit()
    {
        bool passed = true;
        // check to see if the exlusion is voilated
        for(int i = 0; i < _usedpeople.Count; i++)
        {
            switch (_exclusionType)
            {
                case Exclusion.Gender:
                    if (_toggleList[i].isOn && _usedpeople[i].GetGender() == _excludedPerson.GetGender()) passed = false;
                    if (!_toggleList[i].isOn && _usedpeople[i].GetGender() != _excludedPerson.GetGender()) passed = false;


                    break;
                case Exclusion.Age:
                    if (_toggleList[i].isOn && _usedpeople[i].GetAge() > _excludedPerson.GetAge()) passed = false;
                    if (!_toggleList[i].isOn && _usedpeople[i].GetAge() <= _excludedPerson.GetAge()) passed = false;

                    break;
                case Exclusion.BloodType:
                    if (_toggleList[i].isOn && _usedpeople[i].GetBloodType() == _excludedPerson.GetBloodType()) passed = false;
                    if (!_toggleList[i].isOn && _usedpeople[i].GetBloodType() != _excludedPerson.GetBloodType()) passed = false;

                    break;
                case Exclusion.Virginity:
                    if (_toggleList[i].isOn && _usedpeople[i].GetVirginity() == _excludedPerson.GetVirginity()) passed = false;
                    if (!_toggleList[i].isOn && _usedpeople[i].GetVirginity() != _excludedPerson.GetVirginity()) passed = false;
                    break;

            }
        }
        if (passed)
        {
            CloseTask(true);
        } else
        {
            FailSubmission();
        }
    }

    public void GenerateCensus()
    {
        // clear list in case this is called again
        _usedpeople.Clear();


        // build people list
        for (int i = 0; i < _toggleList.Count; i++)
        {
            _usedpeople.Add(GeneratePerson());
        }

        // apply info to appropriate toggles
        int index = 0;
        foreach (Toggle t in _toggleList)
        {
            // build text string
            string s = _usedpeople[index].GetName() + " " + _usedpeople[index].GetAge() + _usedpeople[index].GetGender() + ", ";
            s += _usedpeople[index].GetBloodType() + " " + (_usedpeople[index].GetVirginity() == true ? "Virgin" : "Non-Virgin");

            t.GetComponentInChildren<TextMeshProUGUI>().text = s;

            index++;
        }

        // generate Excluded Person + Type
        _excludedPerson = GeneratePerson();
        _exclusionType = (Exclusion) Random.Range(0, (int) Exclusion.Virginity + 1); 

        switch (_exclusionType)
        {
            case Exclusion.Gender:
                _exclusionTextBox.text = "Select all Sanctify targets that are <color=red>not:</color> " + _excludedPerson.GetGender();
                break;
            case Exclusion.Age:
                _exclusionTextBox.text = "Select all Sanctify targets that are <color=red>not</color> older than: " + _excludedPerson.GetAge();
                break;
            case Exclusion.BloodType:
                _exclusionTextBox.text = "Select all Sanctify targets that are <color=red>not:</color> " + _excludedPerson.GetBloodType();
                break;
            case Exclusion.Virginity:
                string s = _excludedPerson.GetVirginity() == true ? "virigins" : "non-virgins";
                _exclusionTextBox.text = "Select all Sanctify targets that are <color=red>not:</color> " + s;
                break;

        }
    }

    private void FailSubmission()
    {
        // try again message goes here
        _faliureWarning.SetActive(true);
        GenerateCensus();
    }

    private PersonSO GeneratePerson()
    {
        PersonSO person = ScriptableObject.CreateInstance<PersonSO>();
        person.SetName(_peopleSO.GetName());
        person.SetAge(_peopleSO.GetAge());
        person.SetGender(_peopleSO.GetGender());
        person.SetBloodType(_peopleSO.GetBloodType());
        person.SetVirginity(_peopleSO.GetVirginity());
        return person;
    }

    protected override void OnDestroy()
    {
        // unsubscribe from stuff
        base.OnDestroy();
    }


    protected override void Update()
    {
        base.Update();
    }

    public override void StartTask()
    {
        base.StartTask();
    }

    public override void CloseTask(bool passed)
    {
        base.CloseTask(passed);
    }
}
