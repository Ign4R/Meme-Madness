using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ManagerBenson : MonoBehaviour
{
    public static ManagerBenson instance;
    public float remainingTime;
    public TextMeshProUGUI timer;
    private int _caughtBalls;
    private float _objective;
    [SerializeField] private TextMeshProUGUI score;
    public DifficultyValuesScriptableObject difficultyValues;
    [SerializeField] GameObject ballSpawner;
    [SerializeField] private Slider sliderTimer;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        sliderTimer.maxValue = remainingTime;
        
        foreach (DifficultyValuesScriptableObject values in GameManager.instance.minigamesDifficultyValues)
            if (values.minigameName == "Benson") difficultyValues = values;

        foreach (MultipleValueVariable val in difficultyValues.variables)
            if (val.variableName == "objective") _objective = val.value[GameManager.instance.currentRound - 1];

        foreach (MultipleValueVariable val in difficultyValues.variables)
            if (val.variableName == "movementVelocity") ballSpawner.GetComponent<BensonController>().movementVelocity = val.value[GameManager.instance.currentRound - 1];

    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            sliderTimer.value = remainingTime;
        }

        if (remainingTime <= 0)
        {
            if (ConditionDefeat()) ConditionAddPoints();
            else
            {
                GameManager.instance.GameOver();
            }
            /*Lose();*/
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timer.text = $"{minutes}:{seconds}";
        score.text = _caughtBalls.ToString();
    }

    public void CatchBall()
    {
        _caughtBalls++;
        AudioManager.AudioInstance.PlaySFX("catchBall");
    }

    bool ConditionDefeat()
    {
        return _caughtBalls >= _objective;
    }

    private void ConditionAddPoints()
    {
        GameManager.instance.LoadNewLevel();
        GameManager.instance.AddPoints(_caughtBalls++);
        Debug.Log("Win");
    }

    public void Lose() => GameManager.instance.GameOver();

}