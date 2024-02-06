using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDragAndDrop : MonoBehaviour
{
    //[SerializeField] private AudioSource _source;
    //[SerializeField] private AudioClip _pickUpClip, _dropClip;
    private bool isDragging = false;
    private GameObject draggedObject;
    private float distanceToCamera;


    void Update()
    {
        if (GameManager.instance.isPaused == true) return;
        // Detectar clic del mouse
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            AudioManager.AudioInstance.PlaySFX("PickUp");
            //_source.PlayOneShot(_pickUpClip);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            // Comprobar si se hizo clic en un objeto
            if (hits.Length > 0)
            {
                // Seleccionar el objeto más cercano en la profundidad
                RaycastHit2D topHit = hits[0];
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.CompareTag("Draggable") && hit.collider.transform.position.z > topHit.collider.transform.position.z)
                    {
                        topHit = hit;
                    }
                }

                isDragging = true;
                draggedObject = topHit.collider.gameObject;
                distanceToCamera = Vector3.Distance(topHit.collider.transform.position, Camera.main.transform.position);
            }
        }

        // Liberar el objeto cuando se suelta el clic
        if (Input.GetMouseButtonUp(0))
        {
            AudioManager.AudioInstance.PlaySFX("Drop");
            //_source.PlayOneShot(_dropClip);
            isDragging = false;
        }

        // Arrastrar el objeto si está siendo arrastrado
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 newPosition = ray.GetPoint(distanceToCamera);
            draggedObject.transform.position = new Vector3(newPosition.x, newPosition.y, draggedObject.transform.position.z);
        }
    }
}