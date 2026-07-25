using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameEndDisplay : MonoBehaviour
{
    // =================== Refrences ===================
    [SerializeField] private GameObject _phoneIcon;
    [SerializeField] private GameObject _endWindow;
    [SerializeField] private GameObject _bossWindow;
    [SerializeField] private GameObject _tentacles;
    [SerializeField] private TextMeshProUGUI _bossMsg;
    [SerializeField] private GameObject _performaceReview;
    [SerializeField] private TextMeshProUGUI _performaceText;
    [SerializeField] private TextMeshProUGUI _statsText;
    private bool _taskOpen = true;

    // =================== Setup ===================
    #region Function
    private void Start()
    {
        GameplayManager.OnGameEndWin += GameEndWin;
        GameplayManager.OnGameEndSanctifyLoss += GameEndSanctifyLoss;
        GameplayManager.OnGameEndTaskLoss += GameEndTaskLoss;
    }

    private void OnDestroy()
    {
        GameplayManager.OnGameEndWin -= GameEndWin;
        GameplayManager.OnGameEndSanctifyLoss -= GameEndSanctifyLoss;
        GameplayManager.OnGameEndTaskLoss -= GameEndTaskLoss;
    }
    #endregion

    // =================== Function ===================
    #region Function
    private void GameEndWin()
    {
        StartCoroutine(DoGameEnd("Shift is over go home... What do you want me to congratulate you? You pressed a button all day.", true, false));
    }

    private void GameEndSanctifyLoss()
    {
        StartCoroutine(DoGameEnd("Your ineptitude has unleashed the avatar of death. This is the end of all life.... You're fired.", false, true));
    }

    private void GameEndTaskLoss()
    {
        StartCoroutine(DoGameEnd("Getting reports from HR and other employees, you're slacking... Pack up, you're fired.", false, false));
    }

    private IEnumerator DoGameEnd(string bossMsg, bool won, bool sanctLoss)
    {
        _phoneIcon.SetActive(true);

        yield return new WaitForSeconds(3f);

        _phoneIcon.SetActive(false);
        _endWindow.SetActive(true);
        _bossWindow.SetActive(true);
        _tentacles.SetActive(sanctLoss);

        _bossMsg.text = bossMsg;

        yield return new WaitForSeconds(6f);

        _phoneIcon.SetActive(false);
        _bossWindow.SetActive(false);
        _performaceReview.SetActive(true);

        if (won)
        {
            _performaceText.text = "Day Complete! - WIN";
            _performaceText.color = Color.green;
        }
        else
        {
            _performaceText.text = "Fired! - LOSE";
            _performaceText.color = Color.red;
        }

        _statsText.text = $"Sanctifications - {StatTracker.Instance.GetNumSanctifications()}\n" +
                            $"Tasks Completed - {StatTracker.Instance.GetNumTasksCompleted()}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ComboScene", LoadSceneMode.Single);
    }
    #endregion
}
