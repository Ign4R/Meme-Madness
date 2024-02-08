using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ColectablesArrays 
{
    public int[] colectablesId;
    public int[] remainingUnlocks;
    public bool[] colectablesIsUnlocked;
}

public class ColectablesManager : MonoBehaviour
{
    public static ColectablesManager instance;
    [SerializeField] private GameObject colectablesMenu; 
    [SerializeField] private Unlockable unlockablePrefab;
    private Image[] colectablesObjects; 
    private ColectablesSo[] colectables;
    public ColectablesArrays arrays = new();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        colectables = Resources.LoadAll<ColectablesSo>("Coleccionables");
        Initialization();
        AssingDefaultValues();
        AssingImages();
        ResetColectablesMenu();


    }

    private void Initialization()
    {
        arrays.colectablesId = new int[colectables.Length];
        arrays.remainingUnlocks = new int[colectables.Length];
        arrays.colectablesIsUnlocked = new bool[colectables.Length];
        
    }

    private void AssingDefaultValues()
    {
        
        for (int i = 0; i < colectables.Length; i++)
        {
            arrays.colectablesId[i] = colectables[i].id;
            arrays.colectablesIsUnlocked[i] = colectables[i].isUnlocked;

        }
        arrays.remainingUnlocks = arrays.colectablesId;
    }

    public void AssingImages()
    {
        colectablesObjects = new Image[colectables.Length];
        for (int i = 0; i < colectables.Length; i++)
        {
            var aux = Instantiate(unlockablePrefab, colectablesMenu.transform);
            colectablesObjects[i] = aux.image;
        }
    }

    public void ClearImages()
    {
        Array.Clear(colectablesObjects,0, colectablesObjects.Length);
    }

    public void ResetColectablesMenu()
    {
        for (int i = 0; i < colectablesObjects.Length; i++)
        {
            if(arrays.colectablesIsUnlocked[i] == false)
            {
                colectablesObjects[i].sprite = colectables[i].sihouetteMeme;
            }
            else
            {
                colectablesObjects[i].sprite = colectables[i].normalMeme;
            }
        }
    }

    public void Save()
    {
        SaveAndLoad.SaveColectables(arrays);
    }

    public void Load()
    {
        ColectableData loadedArrays = SaveAndLoad.LoadColectables();
        arrays.colectablesId = loadedArrays.id;
        arrays.colectablesIsUnlocked = loadedArrays.isUnlocked;
        arrays.remainingUnlocks = loadedArrays.remainingUnlocks;
        ResetColectablesMenu();
    }

}
