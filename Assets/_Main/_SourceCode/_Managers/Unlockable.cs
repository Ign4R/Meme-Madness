using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlockable : MonoBehaviour
{
    public int id;
    public int price;
    public Image image;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(delegate { ColectablesManager.instance.ShowUnlockableInfo(id, price); });
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

}
