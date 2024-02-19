using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRewardsRefresh : MonoBehaviour
{
    [SerializeField] private TMP_Text rewardPointsText;
    [SerializeField] private GameObject colectablesMenu;

    private void Start()
    {
        ColectablesManager.instance.ObjectsReference(colectablesMenu, rewardPointsText);
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
