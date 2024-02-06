using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyValues", menuName = "ScriptableObjects")]
public class DifficultyValuesScriptableObject : ScriptableObject
{
    public string minigameName;
    public MultipleValueVariable[] variables;
}
