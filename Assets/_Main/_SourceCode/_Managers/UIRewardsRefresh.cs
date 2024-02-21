using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRewardsRefresh : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughScore;


    private void Start()
    {
        ColectablesManager.instance.ObjectsReference(notEnoughScore);
    }
    
    public void CloseUnlockableInfo()
    {
        ColectablesManager.instance.CloseUnlockableInfo();

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
