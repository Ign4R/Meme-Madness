using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColectableData
{
    public int score;
    public int[] id;
    public int[] remainingUnlocks;
    public bool[] isUnlocked;

    public ColectableData(ColectablesArrays arrays)
    {
        this.score = arrays.SCORE_SAVED_APPLICATION;
        id = new int[arrays.colectablesId.Length];
        remainingUnlocks = new int[arrays.remainingUnlocks.Length];
        isUnlocked = new bool[arrays.colectablesIsUnlocked.Length];
        id = arrays.colectablesId;
        remainingUnlocks = arrays.remainingUnlocks;
        isUnlocked = arrays.colectablesIsUnlocked;
    }
}
