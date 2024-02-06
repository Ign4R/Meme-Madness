using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentGame;
    private bool _tutorial;
    public int currentRound;
    public int points;
    public List<string> array1;
    public List<string> array2;
    public bool isPaused = false;
    public List<string> games;
    public DifficultyValuesScriptableObject[] minigamesDifficultyValues;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject HUDBenson;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        _tutorial = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            LoadNewLevel();
        } 
        
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeInHierarchy && SceneManagerScript.instance.scene != 0)
            Pause();
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy)
            Resume();
    }

    private void Randomize()
    {
        foreach (string i in games) array1.Add(i);

        array2.Clear();
        int b = games.Count; ///HARCORDIE PORQUE EL VALOR DE ESTA VARIABLE ERA UNA LISTA DE ESCENAS.COUNT
        for (int n = 0; n < b; n++)
        {
            int a = Random.Range(0, array1.Count);
            array2.Add(array1[a]);
            array1.RemoveAt(a);
        }
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
        if (currentRound == 3)
        {
            Win();
        }
        else
        {
            games = array2;
            currentRound++;
            Reshuffle();
            LoadNewLevel();
        }
    }

    public void LoadNewLevel()
    {
        if (currentGame == 4)         //En la ronda 1, los minijuegos se juegan en orden.
        {
            if (_tutorial) _tutorial = false;
            SetNewRound();
        }
        else
        {
            currentGame++;
            if (_tutorial) SceneManagerScript.instance.LoadScene(currentGame);
            else SceneManager.LoadScene(games[currentGame - 1]);
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        Debug.Log($"+{pointsToAdd} pts");
    }
    public void Win()
    {
        Debug.Log("Game over");
        currentGame = 0;
        currentRound = 1;
        _tutorial = true;
        games = array2;
        SceneManager.LoadScene(5);
    }
    public void GameOver()
    {
        Debug.Log("Game over");
        currentGame = 0;
        currentRound = 1;
        _tutorial = true;
        games = array2;
        SceneManager.LoadScene(6);

    }

    public void LoadMainMenu() => SceneManagerScript.instance.LoadScene(0);


    public void ResetPause()
    {
        currentGame = 0;
        currentRound = 1;
        isPaused = false;
        _tutorial = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        games = array2;
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
