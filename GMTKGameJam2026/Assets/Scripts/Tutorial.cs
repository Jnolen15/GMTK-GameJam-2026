using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial : MonoBehaviour
{
    // =================== Variables ===================
    [SerializeField] private List<GameObject> _tutPages = new List<GameObject>();
    [SerializeField] private GameObject _prevButton;
    [SerializeField] private GameObject _nextButton;
    private int _pageIndex;

    // =================== Setup ===================
    #region Setup
    void Start()
    {
        _prevButton.SetActive(false);
    }
    #endregion

    // =================== Function ===================
    #region Function
    public void ChangePage(bool next)
    {
        if (next) _pageIndex++;
        else _pageIndex--;

        _prevButton.SetActive(true);
        _nextButton.SetActive(true);

        if (_pageIndex <= 0)
        {
            _pageIndex = 0;
            _prevButton.SetActive(false);
        }
        else if (_pageIndex >= _tutPages.Count-1)
        {
            _pageIndex = _tutPages.Count-1;
            _nextButton.SetActive(false);
        }

        foreach (GameObject page in _tutPages)
            page.SetActive(false);

        _tutPages[_pageIndex].SetActive(true);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("ComboScene", LoadSceneMode.Single);
    }
    #endregion
}
