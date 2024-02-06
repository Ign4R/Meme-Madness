using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggeable : MonoBehaviour
{
    //SpriteRenderer _renderer;
    public LayerMask _mask;
    Vector2 difference = Vector2.zero;

    private void Awake()
    {
        //_renderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        //if(_mask)
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }
    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }
}
