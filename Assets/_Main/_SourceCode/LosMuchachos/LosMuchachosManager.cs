using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class LosMuchachosManager : MonoBehaviour
{
    public Animator _anim;
    private static bool SHOW_CONTROLS;
    public GameObject _controls;    
    public static LosMuchachosManager instance;
    public float remainingTime;
    [SerializeField] private int _scoreIncrease = 50;
    private float _objective;
    [SerializeField] private TextMeshProUGUI _uiScore;
    public DifficultyValuesScriptableObject difficultyValues;
    [SerializeField] BensonController benson;
    [SerializeField] private Slider sliderTimer;
    public int _pointsCollected;

    private void Awake()
    {     
        if (SHOW_CONTROLS)
        {
            _anim.enabled = false;
        }    
    }
    private void Start()
    {
     
        if (instance == null) instance = this;
        else Destroy(gameObject);

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