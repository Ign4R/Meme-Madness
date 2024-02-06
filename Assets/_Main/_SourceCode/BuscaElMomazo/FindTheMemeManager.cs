using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindTheMemeManager : MonoBehaviour
{
    public Slider timerBar;
    public float timer, gameDuration;

    void Update()
    {
        timer += Time.deltaTime;
        GameTimer();
    }

    public void GameTimer()
    {
        timerBar.value = timer / gameDuration;
    }
}
