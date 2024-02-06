using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int _score;
    void Start()
    {
        _score = 0;
    }

    public void SumarPuntos()
    {
        _score++;
    }
}
