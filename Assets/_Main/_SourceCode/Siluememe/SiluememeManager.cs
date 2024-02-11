using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SiluememeManager : MonoBehaviour
{
    private GuessBase[] _base;
    [SerializeField] private float maxCorrectGuesses;
    [SerializeField] private float minigameTimer;
    [SerializeField] private float timerBetweenGuesses;
    [SerializeField] private float incorrectTimer;
    [SerializeField] private float questionTimer;
    [SerializeField] private int scoreIncrease = 20;
    [SerializeField] private Image normalImage;
    [SerializeField] private SpriteRenderer silhouette;
    [SerializeField] private GameObject question;
    [SerializeField] private TMP_Text correct;
    [SerializeField] private TMP_Text incorrect;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text minigameTimerText;
    [SerializeField] private CustomButton[] buttons;
    [SerializeField] private SpriteRenderer[] buttonsSprites;

    private GuessBase currentGuess;
    private int correctGuesses;
    private int sillouetteScore;
    private DifficultyValuesScriptableObject difficultyValues;


    private void Start()
    {
        
        _base = Resources.LoadAll<GuessBase>("Siluetas");

        foreach (DifficultyValuesScriptableObject values in GameManager.instance.minigamesDifficultyValues)
            if (values.minigameName == "Silouette")
                difficultyValues = values;

        foreach (MultipleValueVariable val in difficultyValues.variables)
            if (val.variableName == "maxGuessCount")
                maxCorrectGuesses = val.value[GameManager.instance.currentRound - 1];

        AssigningValues();
        Invoke(nameof(RemoveQuestion), questionTimer);
    }

    private void Update()
    {
        minigameTimer -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(minigameTimer % 60);
        minigameTimerText.text = seconds.ToString();
        if(minigameTimer <= 0)
        {
            DisableButtons();
            GameManager.instance.AddPoints(sillouetteScore);
            GameManager.instance.LoadNewLevel();

        }
    }

    public void RemoveQuestion()
    {
        question.SetActive(false);
    }

    public GuessBase GetRandomGuess()
    {
        var random = Random.Range(0, _base.Length);
        return _base[random];
    }

    public void ChoiceSucess()
    {
        ClearListeners();
        AudioManager.AudioInstance.PlaySFX("bien");
        sillouetteScore += scoreIncrease;
        scoreText.text = sillouetteScore.ToString();
        normalImage.gameObject.SetActive(true);
        silhouette.gameObject.SetActive(false);
        correct.gameObject.SetActive(true);
        DisableButtons();
        correctGuesses++;

        Invoke(nameof(NextGuess), timerBetweenGuesses);

        
    }
    public void IncorrectGuess()
    {
        ClearListeners();
        AudioManager.AudioInstance.PlaySFX("mal");
        normalImage.gameObject.SetActive(true);
        silhouette.gameObject.SetActive(false);
        incorrect.gameObject.SetActive(true);
        DisableButtons();

        Invoke(nameof(NextGuess), incorrectTimer);

    }

    public void NextGuess()
    {
        //if (correctGuesses == maxCorrectGuesses)
        //{
        //    correctGuesses = 0;
        //    //pasar al proximo minijuego
        //}
        //else
        //{
        //    silhouette.gameObject.SetActive(true);
        //    normalImage.gameObject.SetActive(false);
        //    correct.gameObject.SetActive(false);
        //    AssigningValues();
        //}
        EnableButtons();
        silhouette.gameObject.SetActive(true);
        normalImage.gameObject.SetActive(false);
        correct.gameObject.SetActive(false);
        incorrect.gameObject.SetActive(false);
        AssigningValues();
    }

    

    public void DisableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void EnableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    public void AssigningValues()
    {
        
        currentGuess = GetRandomGuess();
        normalImage.sprite = currentGuess.Normal;
        silhouette.sprite = currentGuess.Silhouette;

        var randomButton = Random.Range(0, 4);
        var stack = new Stack(currentGuess.IncorrectAnswers);

        for (int i = 0; i < 4; i++)
        {
            if (buttons[i] == buttons[randomButton])
            {
                buttons[i].OnMouseClick.AddListener(ChoiceSucess);
                buttonsSprites[i].sprite = currentGuess.CorrectAnswer;
                //buttons[i].SpriteRenderer.sprite = currentGuess.CorrectAnswer;
 
                continue;
            }

            buttons[i].OnMouseClick.AddListener(IncorrectGuess);
            buttonsSprites[i].sprite = (Sprite)stack.Pop();
            //buttons[i].SpriteRenderer.sprite = (Sprite)stack.Pop();
        }
    }

    public void ClearListeners()
    {
        for (int i = 0; i < 4; i++)
        {
            buttons[i].OnMouseClick.RemoveAllListeners();
        }
    }
}
