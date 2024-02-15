using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BuscaElMomazoManager : MonoBehaviour
{
    [SerializeField] private GameObject _transition;
    private int counter;
    public Vector2 posMemesEnemies;
    [SerializeField] private List<MemeSlot> _slotPrefabs;
    [SerializeField] private Meme _memePrefab;
    [SerializeField] private Transform _slotParent, _pieceParent;
    public Slider timerBar;
    public TextMeshProUGUI _uiScore;
    public float timer, gameDuration;
    public float maxMemes;
    [SerializeField] private int scoreIncrease = 30;
    public float maxObjective;

    public List<MemeSlot> listSlots;
    Meme spawnedPiece;
    MemeSlot spawnedSlot;
    public static BuscaElMomazoManager instance;

    public List<GameObject> images = new List<GameObject>();


    public List<Meme> memes = new List<Meme>();
    public List<MemeSlot> memeSlots = new List<MemeSlot>();
    private DifficultyValuesScriptableObject difficultyValues;
    private int _pointsCollected=0;

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

    }

    private void Start()
    {
        _transition.SetActive(true);
        RefreshScore();
        SetDifficulty();
        Spawn();

    }
    public void RefreshScore()
    {
        _uiScore.text = GameManager.instance.MainScored.ToString();
    }
    public void SetDifficulty()
    {
        foreach (DifficultyValuesScriptableObject values in GameManager.instance.minigamesDifficultyValues)
            if (values.minigameName == "FindMeme")
                difficultyValues = values;
        foreach (MultipleValueVariable objective in difficultyValues.variables)
            if (objective.variableName == "maxObjective")
                maxObjective = objective.value[GameManager.instance.currentRound - 1];

        foreach (MultipleValueVariable slots in difficultyValues.variables)
            if (slots.variableName == "slots")
                maxMemes = slots.value[GameManager.instance.currentRound - 1];
    }
    void Spawn()
    {
        listSlots = _slotPrefabs.OrderBy(s => Random.value).Take((int)maxMemes).ToList();

        for (int i = 0; i < listSlots.Count; i++)
        {
            spawnedSlot = Instantiate(listSlots[i], _slotParent.GetChild(i).position, Quaternion.identity);
            spawnedPiece = Instantiate(_memePrefab, _pieceParent.GetChild(i).position, Quaternion.identity);
            memes.Add(spawnedPiece);
            memeSlots.Add(spawnedSlot);
            spawnedPiece.Init(spawnedSlot);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        GameTimer();
        GameTimeOver();
    }
    public void GameTimeOver()
    {
        if(timer > gameDuration)
        {
            GameManager.instance.AddPoints(_pointsCollected);
            GameManager.instance.LoadNewLevel();
        }
    }
    public void CleanScreen()
    {
        counter++;
        if (counter == maxMemes)
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i].transform.position = posMemesEnemies;
            }
            for (int i = 0; i < memes.Count; i++)
            {
                memes[i].gameObject.SetActive(false);
                memeSlots[i].gameObject.SetActive(false);
            }
            Spawn();
            counter = 0;
        }
   
    }
    public void GameTimer()
    {
        timerBar.value = timer / gameDuration;
    }

    public void PlacedSucess()
    {
        _pointsCollected += scoreIncrease;
        _uiScore.text = (_pointsCollected + GameManager.instance.MainScored).ToString();
        AudioManager.AudioInstance.PlaySFX("Coin");
    }
}
