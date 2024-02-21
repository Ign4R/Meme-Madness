using TMPro;
using UnityEngine;

public class ManagerScreens : MonoBehaviour
{
    [SerializeField] private GameObject _transition;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _winGameScreen;
    [SerializeField] private TextMeshProUGUI _uiPointsWin;
    [SerializeField] private TextMeshProUGUI _uiRewardsPoints;
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
            GameManager.instance.AddRewardPoints();
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
        _uiPointsWin.text = GameManager.instance.RewardPoints.ToString();
        _mainMenu.SetActive(false);
        _winGameScreen.SetActive(true);
        _uiRewardsPoints.text = _uiPointsWin.text;
        ColectablesManager.instance.arrays.SCORE_SAVED_APPLICATION = GameManager.instance.RewardPoints;
        ColectablesManager.instance.Save();
        
    }

    public void BackMenu()
    {
        GameManager.instance.ResetRoundAndGame();
        _mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}

