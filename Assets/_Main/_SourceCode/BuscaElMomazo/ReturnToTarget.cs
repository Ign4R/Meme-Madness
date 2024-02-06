using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToTarget : MonoBehaviour
{
    public GameObject _target;
    public float _speed;

    Vector2 _current;
    float initZ;
    private DifficultyValuesScriptableObject difficultyValues;

    private void Awake()
    {
        initZ = transform.position.z;
    }
    private void Start()
    {
        foreach (DifficultyValuesScriptableObject values in GameManager.instance.minigamesDifficultyValues)
            if (values.minigameName == "FindMeme")
                difficultyValues = values;
        foreach (MultipleValueVariable speed in difficultyValues.variables)
            if (speed.variableName == "speed")
                _speed = speed.value[GameManager.instance.currentRound - 1];

    }
    void Update()
    {
        _current = new Vector3(transform.position.x, transform.position.y, initZ);
        var step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(_current, _target.transform.position, step);
        transform.position = new Vector3(transform.position.x, transform.position.y, initZ);
    }
}
