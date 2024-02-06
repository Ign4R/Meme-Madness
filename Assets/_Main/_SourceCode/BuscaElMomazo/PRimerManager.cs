using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRimerManager : MonoBehaviour
{
    public Stack<GameObject> _pila = new Stack<GameObject>();
    public GameObject meme;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public float range;
    Vector2 dir;

    private void Awake()
    {
        Rellenar();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) Desapilar();

        //if(_pila.Count == 1) 
    }
    public void Rellenar()
    {
        _pila.Push(meme);
        _pila.Push(item1);
        _pila.Push(item2);
        _pila.Push(item3);
        _pila.Push(item4);
        _pila.Push(item5);
    }

    public void BuscarDireccion()
    {
        var x2 = Random.Range(-range, range);
        var y2 = Random.Range(-range, range);

        var x = Random.Range(-x2, x2);
        var y = Random.Range(-y2, y2);

        if (x > -6 && x < 0) x = -6;
        if (x >= 0 && x < 6) x = 6;

        if (y > -6 && y < 0) y = -6;
        if (y >= 0 && y < 6) y = 6;

        dir = new Vector2(x, y);
    }
    public void Desapilar()
    {
        BuscarDireccion();

        _pila.Peek().transform.position = dir;

        _pila.Pop();
    }
}
