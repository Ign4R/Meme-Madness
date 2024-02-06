using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meme : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    private bool _dragging, _placed;
    private Vector2 _offset, _originalPosition;

    private MemeSlot _slot;

    public void Init(MemeSlot slot)
    {
        _renderer.sprite = slot.Renderer.sprite;
        _slot = slot;
    }
    private void Awake()
    {
        _originalPosition = transform.position;
    }
    private void Update()
    {
        if (_placed) return;
        if (!_dragging) return;

        var mousePosition = GetMousePos();
        transform.position = mousePosition - _offset;
    }
    private void OnMouseDown()
    {
        if (GameManager.instance.isPaused == true) return;
        _dragging = true;
        AudioManager.AudioInstance.PlaySFX("PickUp");
        //_source.PlayOneShot(_pickUpClip);
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        if (GameManager.instance.isPaused == true) return;
        if (Vector2.Distance(transform.position, _slot.transform.position) < 1)
        {
            transform.position = _slot.transform.position;
            _slot.Placed();
            PutTheMemeManager._PutTheMemeInstance.currentPoints++;
            PutTheMemeManager._PutTheMemeInstance.counter++;
            _placed = true;
        }
        else
        {
            transform.position = _originalPosition;
            AudioManager.AudioInstance.PlaySFX("Drop");
            //_source.PlayOneShot(_dropClip);
            _dragging = false;
        }
    }
    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
