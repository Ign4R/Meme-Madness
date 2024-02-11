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
    private int _caughtBalls;
    private float _objective;
    [SerializeField] private TextMeshProUGUI score;
    public DifficultyValuesScriptableObject difficultyValues;
    [SerializeField] BensonController benson;
    [SerializeField] private Slider sliderTimer;
    public int pointsCollected;

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

        foreach (DifficultyValuesScriptableObject values in GameManager.instance.minigamesDifficultyValues)
            if (values.minigameName == "Benson") difficultyValues = values;

        foreach (MultipleValueVariable val in difficultyValues.variables)
            if (val.variableName == "objective") _objective = val.value[GameManager.instance.currentRound - 1];

        foreach (MultipleValueVariable val in difficultyValues.variables)
            if (val.variableName == "movementVelocity") benson.movementVelocity = val.value[GameManager.instance.currentRound - 1];

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
                ExitMiniGame();
                /*Lose();*/
            }

        }
    }

    public void CaughtSuccess()
    {
        _caughtBalls++;
        pointsCollected = 8 * _caughtBalls;
        score.text= _caughtBalls.ToString();
        AudioManager.AudioInstance.PlaySFX("catchBall");
    }

    bool ConditionDefeat()
    {
        return _caughtBalls >= _objective;
    }

    private void ExitMiniGame()
    {
      
        GameManager.instance.LoadNewLevel();
        GameManager.instance.AddPoints(pointsCollected);
        Debug.Log("Win");
    }

    public void FillSlide()
    {
        sliderTimer.maxValue = remainingTime;
        sliderTimer.value = remainingTime;
    }
    //public void Lose() => GameManager.instance.GameOver();

}