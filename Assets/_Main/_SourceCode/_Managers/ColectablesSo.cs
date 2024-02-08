using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colectable", menuName = "Create new colectable")]
public class ColectablesSo: ScriptableObject
{
    public int id;
    public bool isUnlocked;
    public Sprite normalMeme;
    public Sprite sihouetteMeme;
}

