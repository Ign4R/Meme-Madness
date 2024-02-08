using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColectableData
{
    public int[] id;
    public int[] remainingUnlocks;
    public bool[] isUnlocked;

    public ColectableData(ColectablesArrays arrays)
    {
        id = new int[arrays.colectablesId.Length];
        remainingUnlocks = new int[arrays.remainingUnlocks.Length];
        isUnlocked = new bool[arrays.colectablesIsUnlocked.Length];
        id = arrays.colectablesId;
        remainingUnlocks = arrays.remainingUnlocks;
        isUnlocked = arrays.colectablesIsUnlocked;
    }
}
