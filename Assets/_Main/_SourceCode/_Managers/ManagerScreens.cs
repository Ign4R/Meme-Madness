using TMPro;
using UnityEngine;

public class ManagerScreens : MonoBehaviour
{
    [SerializeField] private GameObject _transition;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _winGameScreen;
    [SerializeField] private TextMeshProUGUI _uiScoreWin;
    [SerializeField] private GameObject[] _rounds= new GameObject[2];

    private void Start()
    {
        _transition.SetActive(true);
        if (GameManager.instance.isDefeat)
        {
            ShowInfoGameDefeat();
        }
        if (GameManager.instance.isWin)
        {
            GameManager.instance.SavedScore();
            ShowInfoGameWin();
        }
       
    }
    public void ShowInfoGameDefeat()
    {
        _mainMenu.SetActive(false);
        _gameOverScreen.SetActive(true);
        var currentRound = GameManager.instance.currentRound-1;
        if (currentRound <= _rounds.Length)
        {
            for (int i = 0; i < currentRound; i++)
            {
                _rounds[i].SetActive(true);
            }
        }
    }
    public void ShowInfoGameWin()
    {     
        _uiScoreWin.text= GameManager.instance.ScoreSaved.ToString();
        _mainMenu.SetActive(false);
        _winGameScreen.SetActive(true);
        
    }
    public void BackMenu()
    {
        GameManager.instance.ResetRoundAndGame();
        _mainMenu.SetActive(true);
    }
}

