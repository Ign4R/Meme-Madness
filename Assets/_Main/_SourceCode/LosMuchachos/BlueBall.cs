using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBall : Ball
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Mordecai"))
           Collided(true);
        else 
        {
            if (col.gameObject.CompareTag("Rigby"))
                AudioManager.AudioInstance.PlaySFX("Escupir_1");
            Collided(false);
        }
        
    }
}
