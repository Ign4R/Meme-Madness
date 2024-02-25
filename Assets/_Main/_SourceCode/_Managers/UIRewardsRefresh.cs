using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRewardsRefresh : MonoBehaviour
{

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
