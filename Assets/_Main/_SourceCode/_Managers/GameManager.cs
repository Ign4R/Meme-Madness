using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentGame;
    private bool _tutorial;
    public bool isDefeat;
    public bool isWin;
    public int currentRound;
    public int MainScored { get; private set; }
    public int RewardPoints { get; private set; } ///Puntos de canje


    public bool isPaused = false;
    public List<string> games;
    public DifficultyValuesScriptableObject[] minigamesDifficultyValues;
    [SerializeField] private GameObject pauseMenu;

    public int[] scorePerRound;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        _tutorial = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) &&  SceneManagerScript.instance.scene != 0)
        {
            LoadNewLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeInHierarchy && SceneManagerScript.instance.scene != 0)
            Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy)
            Resume();
    }

    private void Reshuffle()
    {
        for (int t = 0; t < games.Count; t++)
        {
            var tmp = games[t];
            int r = Random.Range(t, games.Count);
            games[t] = games[r];
            games[r] = tmp;
        }
    }
    void SetNewRound()
    {
        currentGame = 0;
        if (_tutorial)
        {
            _tutorial = false;
        }
        if (currentRound == 3)
        {
            Win();
        }
        else
        {
            currentRound++;
            Reshuffle();
            LoadNewLevel();
        }
    }
    public void LoadNewLevel()
    {
       
        if (currentGame == 4)        //En la ronda 1, los minijuegos se juegan en orden.
        {               
            if (MainScored >= scorePerRound[currentRound - 1])
            {
                SetNewRound();
            }
            else
            {
                GameOver();
            }
        }
        else
        {
            currentGame++;
            if (_tutorial)
            {
                SceneManagerScript.instance.LoadScene(currentGame);
            }
            else
            {
                SceneManager.LoadScene(games[currentGame - 1]);
            }
        }
    }
    public void AddPoints(int pointsToAdd)
    {
        MainScored += pointsToAdd;
        Debug.Log($"+{pointsToAdd} pts");
    }
    public void Win()
    {
        Debug.Log("Game over");
        _tutorial = true;
        isWin = true;
        SceneManagerScript.instance.LoadScene(0);
    }
    public void GameOver()
    {
        Debug.Log("Game over");
        isDefeat = true;
        _tutorial = true;
        SceneManagerScript.instance.LoadScene(0);
    }
    public void ResetRoundAndGame()
    {
        currentGame = 0;
        currentRound = 1;
    }
    public void LoadMainMenu() ///Lo utiliza el boton pausa
    {
        ResetRoundAndGame();
        Time.timeScale = 1;
        _tutorial = true;
        currentRound = 1;
        pauseMenu.SetActive(false);
        SceneManagerScript.instance.LoadScene(0);
    }
    public void AddRewardPoints()
    {
        RewardPoints += MainScored;
        MainScored = 0;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
