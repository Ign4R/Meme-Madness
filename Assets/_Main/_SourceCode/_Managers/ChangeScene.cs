using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    private void Start()
    {
    
    }
    public void PlayGame()
    {
        GameManager.instance.LoadNewLevel();
    }

    public void Win()
    {

    }

    public void GameOver()
    {
        
    }
}
