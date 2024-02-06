using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class PutTheMemeManager : MonoBehaviour
{
    [SerializeField] private List<MemeSlot> _slotPrefabs;
    [SerializeField] private Meme _memePrefab;
    [SerializeField] private Transform _slotParent, _pieceParent;
    public Slider timerBar;
    public float timer, gameDuration;
    public float memesToSpawn;
    public int counter, currentPoints;
    public float maxObjective;

    public List<MemeSlot> randomSet;
    Meme spawnedPiece;
    MemeSlot spawnedSlot;
    public static PutTheMemeManager _PutTheMemeInstance;

    public List<GameObject> images = new List<GameObject>();


    public List<Meme> memes = new List<Meme>();
    public List<MemeSlot> memeSlots = new List<MemeSlot>();
    private DifficultyValuesScriptableObject difficultyValues;

    private void Awake()
    {
        if (_PutTheMemeInstance == null)
        {
            _PutTheMemeInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Spawn();
    }

    private void Start()
    {
        foreach (DifficultyValuesScriptableObject values in GameManager.instance.minigamesDifficultyValues)
            if (values.minigameName == "FindMeme")
                difficultyValues = values;
        foreach (MultipleValueVariable objective in difficultyValues.variables)
            if (objective.variableName == "maxObjective")
                maxObjective = objective.value[GameManager.instance.currentRound - 1];

        foreach (MultipleValueVariable slots in difficultyValues.variables)
            if (slots.variableName == "slots")
                memesToSpawn = slots.value[GameManager.instance.currentRound - 1];
    }

    void Spawn()
    {
        randomSet = _slotPrefabs.OrderBy(s => Random.value).Take((int)memesToSpawn).ToList();

        for (int i = 0; i < randomSet.Count; i++)
        {
            spawnedSlot = Instantiate(randomSet[i], _slotParent.GetChild(i).position, Quaternion.identity);
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
        CleanScreen();
        TimeOut();
    }
    public void TimeOut()
    {
        if(timer > 15)
        {
            if (currentPoints < maxObjective) 
            {
                //restar vida
            }
       
            GameManager.instance.LoadNewLevel();
            GameManager.instance.AddPoints(currentPoints);
        }
    }

    public void CleanScreen()
    {
        if(counter == randomSet.Count)
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i].transform.position = new Vector2(0, 2);
            }
            for (int i = 0; i < memes.Count; i++)
            {
                memes[i].gameObject.SetActive(false);
                memeSlots[i].gameObject.SetActive(false);
            }

            counter = 0;
            Spawn();
        }
    }
    public void GameTimer()
    {
        timerBar.value = timer / gameDuration;
    }
}
