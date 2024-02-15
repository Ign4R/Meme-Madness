using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScoreManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _uiScore;
   private int _scoreSave;

    public int ScoreSaved { get => _scoreSave; set => _scoreSave = value; }

    private void Start()
    {
        _scoreSave = GameManager.instance.MainScored;
        _uiScore.text = _scoreSave.ToString();
    }

    public void LoadMenu()
    {
        SceneManagerScript.instance.LoadScene(0);
    }
}
