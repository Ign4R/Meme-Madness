using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    void NextScene()
    {
        GameManager.instance.LoadNewLevel();
    }
}
