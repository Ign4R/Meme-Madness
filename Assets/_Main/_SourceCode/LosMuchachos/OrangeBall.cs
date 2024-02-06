using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBall : Ball
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Rigby"))
            Collided(true);
        else 
        {
            if (col.gameObject.CompareTag("Mordecai"))
                AudioManager.AudioInstance.PlaySFX("Escupir_1");
            Collided(false);
        }
    }
}
