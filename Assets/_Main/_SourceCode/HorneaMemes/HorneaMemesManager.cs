using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HorneaMemesManager : MonoBehaviour
{
    [SerializeField] private float maxCorrectAnswers;
    [SerializeField] private int scoreIncrease=10;
    [SerializeField] private float minigameTimer;
    [SerializeField] private float timerBetweenPhrases;
    [SerializeField] private float incorrectTimer;
    [SerializeField] private float numberOfMistakes;
    [SerializeField] private TMP_Text _uiScore;
    [SerializeField] private TMP_Text minigameTimerText;
    [SerializeField] private TMP_Text originalPhrase;
    [SerializeField] private TMP_Text correct;
    [SerializeField] private TMP_Text incorrect;
    [SerializeField] private TMP_Text incompletePhrase;
    [SerializeField] private SpriteRenderer memeImage;
    [SerializeField] private CustomButton[] buttons;
    [SerializeField] private TMP_Text[] buttonsText;
    private PhraseData[] phraseData;
    private List<PhraseData> remainings;
    private PhraseData currentPhrase;
    private int _pointsCollected;
    private float correctAnswers;
    private float numberOfMistakesSet;
    private DifficultyValuesScriptableObject difficultyValues;

    private void Start()
    {
        RefreshScore();
        phraseData = Resources.LoadAll<PhraseData>("Frases");
        remainings = new List<PhraseData>(phraseData);
        numberOfMistakesSet = numberOfMistakes;
        SetDifficulty();
        InitializeValues();
    }
    public void SetDifficulty()
    {
        foreach (DifficultyValuesScriptableObject values in GameManager.instance.minigamesDifficultyValues)
            if (values.minigameName == "MakeMeme")
                difficultyValues = values;

        foreach (MultipleValueVariable maxCorrectAnswers in difficultyValues.variables)
            if (maxCorrectAnswers.variableName == "maxCorrectAnswers")
                this.maxCorrectAnswers = maxCorrectAnswers.value[GameManager.instance.currentRound - 1];

        foreach (MultipleValueVariable numberOfMistakes in difficultyValues.variables)
            if (numberOfMistakes.variableName == "numberOfMistakes")
                this.numberOfMistakes = numberOfMistakes.value[GameManager.instance.currentRound - 1];
    }
    public void RefreshScore()
    {
        _uiScore.text = GameManager.instance.MainScored.ToString();
    }
    private void Update()
    {

        minigameTimer -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(minigameTimer % 60);
        minigameTimerText.text = seconds.ToString();
        if (minigameTimer <= 0)
        {
            DisableButtons();
            GameManager.instance.AddPoints(_pointsCollected);
            GameManager.instance.LoadNewLevel();

        }
    }

    public PhraseData GetRandomPhrase()
    {
        if (remainings.Count == 0)
        {
            ResetIndices();
        }

        var randomIndex = Random.Range(0, remainings.Count);
        var randomPhrase = remainings[randomIndex];
        remainings.RemoveAt(randomIndex);

        return randomPhrase;
    }
    private void ResetIndices()
    {
        remainings.AddRange(phraseData);
    }

    public void CompleteSucess()
    {
        AudioManager.AudioInstance.PlaySFX("bien");
        ClearListeners();
        _pointsCollected += scoreIncrease;
        _uiScore.text = (_pointsCollected + GameManager.instance.MainScored).ToString();
        incompletePhrase.gameObject.SetActive(false);
        originalPhrase.gameObject.SetActive(true);
        correct.gameObject.SetActive(true);
        DisableButtons();
        correctAnswers++;

        Invoke(nameof(NextPhrase), timerBetweenPhrases);
    }

    public void IncorrectPhrase(int buttomNumber)
    {
        AudioManager.AudioInstance.PlaySFX("mal");
        if (numberOfMistakes > 0)
        {
            buttons[buttomNumber].gameObject.SetActive(false);
            numberOfMistakes--;
            return;
        }
        ClearListeners();
        incompletePhrase.gameObject.SetActive(false);
        originalPhrase.gameObject.SetActive(true);
        incorrect.gameObject.SetActive(true);
        DisableButtons();

        Invoke(nameof(NextPhrase), incorrectTimer);
    }

    public void NextPhrase()
    {
        numberOfMistakes = numberOfMistakesSet;
        EnableButtons();
        correct.gameObject.SetActive(false);
        incorrect.gameObject.SetActive(false);
        originalPhrase.gameObject.SetActive(false);
        incompletePhrase.gameObject.SetActive(true);
        InitializeValues();
    }

    public void InitializeValues()
    {
        currentPhrase = GetRandomPhrase();
        memeImage.sprite = currentPhrase.MemeImage;
        incompletePhrase.text = currentPhrase.IncompletePhrase;
        var indexRandom = Random.Range(0, 4);
        var stack = new Stack(currentPhrase.IncorrectWords);
        for (int i = 0; i < 4; i++)
        {
            if (buttons[i] == buttons[indexRandom])
            {
                buttons[i].OnMouseClick.AddListener(CompleteSucess);
                originalPhrase.text = currentPhrase.OriginalPhrase;
                buttonsText[i].text = currentPhrase.ChooseCorrect;
                continue;

            }

            buttons[i].OnMouseClickNumber.AddListener(IncorrectPhrase);
            buttons[i].buttonNumber = i;
            buttonsText[i].text = (string)stack.Pop();      
        }

    }
    public void ClearListeners()
    {
        for (int i = 0; i < 4; i++)
        {
            buttons[i].OnMouseClick.RemoveAllListeners();
        }

        for (int i = 0; i < 4; i++)
        {
            buttons[i].OnMouseClickNumber.RemoveAllListeners();
        }
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

}
