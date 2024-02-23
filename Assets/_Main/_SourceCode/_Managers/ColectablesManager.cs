using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ColectablesArrays 
{
    public int SCORE_SAVED_APPLICATION;
    public int[] colectablesId;
    public int[] remainingUnlocks;
    public bool[] colectablesIsUnlocked;
}

public class ColectablesManager : MonoBehaviour
{
    public static ColectablesManager instance;
    [SerializeField] private GameObject colectablesMenu; 
    [SerializeField] private GameObject notEnoughScore; 
    [SerializeField] private Unlockable unlockablePrefab;
    [SerializeField] private TMP_Text rewardPointsText;
    private Unlockable[] colectablesObjects; 
    private ColectablesSo[] colectables;
    private int unlockableIdButton;
    private int price;
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
            colectablesObjects[i].price = colectables[i].price;
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
        GameManager.instance.RewardPoints = arrays.SCORE_SAVED_APPLICATION;
        rewardPointsText.text = arrays.SCORE_SAVED_APPLICATION.ToString();
    }

    public void CloseUnlockableInfo()
    {
        notEnoughScore.SetActive(false);
    }

    public void PurchaseColectable(int unlockableIdButton, int price)
    {
        this.unlockableIdButton = unlockableIdButton;
        this.price = price;
        if (arrays.colectablesIsUnlocked[unlockableIdButton] != true)
        {
            if (arrays.SCORE_SAVED_APPLICATION < price)
            {
                notEnoughScore.SetActive(true);
            }
            else
            {
                arrays.SCORE_SAVED_APPLICATION -= price;
                arrays.colectablesIsUnlocked[unlockableIdButton] = true;
                ResetColectablesMenu();
                CloseUnlockableInfo();
            }
        }
     
       
    }


    public void ObjectsReference(/*GameObject colectablesMenu, GameObject purchaseUnlockableMenu, */GameObject notEnoughScore/*, TMP_Text rewardPointsText*/)
    {
        //this.colectablesMenu = colectablesMenu;
        //this.purchaseUnlockableMenu = purchaseUnlockableMenu;
        //this.rewardPointsText = rewardPointsText;
        this.notEnoughScore = notEnoughScore;
    }

    public void Save()
    {
        SaveAndLoad.SaveColectables(arrays);
    }

    public void Load()
    {
        ColectableData loadedArrays = SaveAndLoad.LoadColectables();
        if ( loadedArrays != null )
        {
            arrays.SCORE_SAVED_APPLICATION = loadedArrays.score;
            arrays.colectablesId = loadedArrays.id;
            arrays.colectablesIsUnlocked = loadedArrays.isUnlocked;
            arrays.remainingUnlocks = loadedArrays.remainingUnlocks;
            ResetColectablesMenu();
        }
    }

}
