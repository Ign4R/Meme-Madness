using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeSlot : MonoBehaviour
{
    public SpriteRenderer Renderer;

    public void Placed()
    {
        AudioManager.AudioInstance.PlaySFX("Coin");
    }
}
