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

        Vector3 mousePosition = GetMousePos();
        Vector3 normalizedDirection = (mousePosition - transform.position).normalized;
        transform.position += normalizedDirection * Time.deltaTime * 10f; // Puedes ajustar la velocidad según sea necesario
    }


    private void OnMouseUp()
    {
        if (GameManager.instance.isPaused == true) return;
        if (Vector2.Distance(transform.position, _slot.transform.position) < 1)
        {
            transform.position = _slot.transform.position;
            _slot.Placed();
            BuscaElMomazoManager.instance.PlacedSucess();
            BuscaElMomazoManager.instance.CleanScreen();
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
