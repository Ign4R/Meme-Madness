using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class LosMuchachosManager : MonoBehaviour
{
    [SerializeField] private GameObject _transition;
    public Animator _anim;
    private static bool SHOW_CONTROLS;
    public GameObject _controls;
    public float remainingTime;
    [SerializeField] private int _scoreIncrease = 50;
    [SerializeField] private TextMeshProUGUI _uiScore;
    public DifficultyValuesScriptableObject difficultyValues;
    [SerializeField] BensonController benson;
    [SerializeField] private Slider sliderTimer;
    public int _pointsCollected;

    public static LosMuchachosManager Instance { get ; set ; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (SHOW_CONTROLS)
        {
            _anim.enabled = false;
        }

        _transition.SetActive(true);
        FillSlide();
        SetDifficulty();
        RefreshScore();
    }
    public void SetDifficulty()
    {
        foreach (DifficultyValuesScriptableObject values in GameManager.instance.minigamesDifficultyValues)
            if (values.minigameName == "Benson") difficultyValues = values;

        foreach (MultipleValueVariable val in difficultyValues.variables)
            if (val.variableName == "cooldown") benson.ballSpawnCd = val.value[GameManager.instance.currentRound - 1];

        foreach (MultipleValueVariable val in difficultyValues.variables)
            if (val.variableName == "movementVelocity") benson.movementVelocity = val.value[GameManager.instance.currentRound - 1];
    }
    public void RefreshScore()
    {
        _uiScore.text = GameManager.instance.MainScored.ToString();
    }
    private void Update()
    {
        if (benson.EndTimeTutorial)
        {
            if (remainingTime > 0)
            {
                SHOW_CONTROLS = true;
                remainingTime -= Time.deltaTime;
                sliderTimer.value = remainingTime;
            }

            if (remainingTime <= 0)
            {
                GameTimeOver();
            }

        }
    }

    public void CaughtSuccess()
    {
        _pointsCollected += _scoreIncrease;
        _uiScore.text = (_pointsCollected + GameManager.instance.MainScored).ToString();
        AudioManager.AudioInstance.PlaySFX("catchBall");
    }

    private void GameTimeOver()
    {     
        GameManager.instance.AddPoints(_pointsCollected);
        GameManager.instance.LoadNewLevel();
        Debug.Log("Win");
    }

    public void FillSlide()
    {
        sliderTimer.maxValue = remainingTime;
        sliderTimer.value = remainingTime;
    }
    //public void Lose() => GameManager.instance.GameOver();

}