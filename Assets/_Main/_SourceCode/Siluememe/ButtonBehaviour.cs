using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float alphaChangeSpeed;
    private SpriteRenderer spriteRenderer;
    private Color color = new Color(255,255,255);
    private Transform pos;
    private Rigidbody2D rb;
    private Vector3 randomDir;
    private int randomRot;
    private float randomAlpha;
    private DifficultyValuesScriptableObject difficultyValues;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        foreach (DifficultyValuesScriptableObject values in GameManager.instance.minigamesDifficultyValues)
            if (values.minigameName == "Silouette")
                difficultyValues = values;

        foreach (MultipleValueVariable speed in difficultyValues.variables)
            if (speed.variableName == "speed")
                this.speed = speed.value[GameManager.instance.currentRound - 1];

        foreach (MultipleValueVariable rotSpeed in difficultyValues.variables)
            if (rotSpeed.variableName == "rotSpeed")
                this.rotSpeed = rotSpeed.value[GameManager.instance.currentRound - 1];

        foreach (MultipleValueVariable alphaChangeSpeed in difficultyValues.variables)
            if (alphaChangeSpeed.variableName == "alphaChangeSpeed")
                this.alphaChangeSpeed = alphaChangeSpeed.value[GameManager.instance.currentRound - 1];
    }

    private void OnEnable()
    {
        GetRandomDirection();
        GetRandomRotation();
        GetRandomAlpha();
        rb.AddForce(randomDir * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, randomRot * rotSpeed * Time.deltaTime);
        color.a += alphaChangeSpeed * randomAlpha * Time.deltaTime;
        spriteRenderer.color = color;
        if(color.a > 1 || color.a < 0)
        {
            alphaChangeSpeed *= -1f;
        }
       
    }

    public void GetRandomDirection()
    { 
        randomDir.x = Random.Range(0, 2);
        randomDir.y = Random.Range(0, 2);
        randomDir.z = 0f;
    }

    public void GetRandomRotation()
    {
        randomRot = Random.Range(-1, 2) * 2-1;
    }

    public void GetRandomAlpha()
    {
        randomAlpha = Random.Range(0.1f, 1f);
    }
}
