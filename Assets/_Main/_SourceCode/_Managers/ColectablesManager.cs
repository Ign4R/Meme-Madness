using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ColectablesArrays 
{
    public int score;
    public int[] colectablesId;
    public int[] remainingUnlocks;
    public bool[] colectablesIsUnlocked;
}

public class ColectablesManager : MonoBehaviour
{
    public static ColectablesManager instance;
    [SerializeField] private GameObject colectablesMenu; 
    [SerializeField] private GameObject purchaseUnlockableMenu; 
    [SerializeField] private Unlockable unlockablePrefab;
    [SerializeField] private TMP_Text rewardPointsText;
    private Unlockable[] colectablesObjects; 
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
        InitializeImages();
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

    public void InitializeImages()
    {
        colectablesObjects = new Unlockable[colectables.Length];
        for (int i = 0; i < colectables.Length; i++)
        {
            var aux = Instantiate(unlockablePrefab, colectablesMenu.transform);
            colectablesObjects[i] = aux;
            colectablesObjects[i].id = i;
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
                colectablesObjects[i].image.sprite = colectables[i].sihouetteMeme;
            }
            else
            {
                colectablesObjects[i].image.sprite = colectables[i].normalMeme;
            }
        }
        GameManager.instance.RewardPoints = arrays.score;
        rewardPointsText.text = arrays.score.ToString();
    }

    public void ShowUnlockableInfo()
    {
        purchaseUnlockableMenu.SetActive(true);
    }

    public void CloseUnlockableInfo()
    {
        purchaseUnlockableMenu.SetActive(false);

    }

    public void ObjectsReference(GameObject colectablesMenu, TMP_Text rewardPointsText)
    {
        this.colectablesMenu = colectablesMenu;
        this.rewardPointsText = rewardPointsText;
    }

    public void Save()
    {
        SaveAndLoad.SaveColectables(arrays);
    }

    public void Load()
    {
        ColectableData loadedArrays = SaveAndLoad.LoadColectables();
        arrays.score = loadedArrays.score;
        arrays.colectablesId = loadedArrays.id;
        arrays.colectablesIsUnlocked = loadedArrays.isUnlocked;
        arrays.remainingUnlocks = loadedArrays.remainingUnlocks;
        ResetColectablesMenu();
    }

}
