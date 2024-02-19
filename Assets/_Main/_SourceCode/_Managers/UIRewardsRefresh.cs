using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRewardsRefresh : MonoBehaviour
{
    [SerializeField] private TMP_Text rewardPointsText;
    [SerializeField] private GameObject colectablesMenu;
    [SerializeField] private GameObject purchaseUnlockableMenu;
    [SerializeField] private GameObject notEnoughScore;


    private void Start()
    {
        ColectablesManager.instance.ObjectsReference(colectablesMenu, purchaseUnlockableMenu, notEnoughScore, rewardPointsText);
    }

    public void CloseUnlockableInfo()
    {
        ColectablesManager.instance.CloseUnlockableInfo();

    }
    public void PurchaseColectable()
    {
        ColectablesManager.instance.PurchaseColectable();
    }

    private void OnEnable()
    {
        ColectablesManager.instance.Load();
    }

    private void OnDisable()
    {
        ColectablesManager.instance.Save();
    }
}
