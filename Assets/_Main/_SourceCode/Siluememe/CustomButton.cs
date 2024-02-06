using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomIntEvent : UnityEvent<int>
{
}
public class CustomButton : MonoBehaviour
{
    private UnityEvent onMouseClick = new UnityEvent();
    private CustomIntEvent onMouseClickNumber = new CustomIntEvent();
    public UnityEvent OnMouseClick => onMouseClick;
    public CustomIntEvent OnMouseClickNumber => onMouseClickNumber;
    public int buttonNumber;

    public void OnMouseDown()
    {
        if (GameManager.instance.isPaused == true) return;
        onMouseClick?.Invoke();
        onMouseClickNumber?.Invoke(buttonNumber);
    }

}
