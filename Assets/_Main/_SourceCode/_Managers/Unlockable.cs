using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unlockable : MonoBehaviour
{
    public int id;
    public int price;
    public Image image;
    private Button button;
    public TextMeshProUGUI uiPrice;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        uiPrice.text = price.ToString();    
        button.onClick.AddListener(delegate { ColectablesManager.instance.PurchaseColectable(id, price); });
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

}
