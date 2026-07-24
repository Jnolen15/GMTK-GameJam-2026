using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class MainTaskTimerUI : MonoBehaviour
{

    // ------------------------------------- Variables -------------------------------------


    [SerializeField] private string TaskName = "Main";

    private TextMeshProUGUI _textBox;

    // ------------------------------------- Functions -------------------------------------

    void Start()
    {
        //subscribe to events
        GameplayManager.OnMainTimerUpdate += UpdateText;

        _textBox = this.GetComponent<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        // unsubscruibe from events
        GameplayManager.OnMainTimerUpdate += UpdateText;
    }

    void UpdateText(float input)
    {
        _textBox.text = input .ToString("#.00");
    }
}
