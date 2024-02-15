using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float fallSpeed;

    private void Start()
    {
        foreach (MultipleValueVariable val in LosMuchachosManager.Instance.difficultyValues.variables)
            if (val.variableName == "fallSpeed") fallSpeed = val.value[GameManager.instance.currentRound - 1];
    }
    
    private void Update() => transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

    protected void Collided(bool withPlayer)
    {
        if(withPlayer) LosMuchachosManager.Instance.CaughtSuccess();
        Destroy(gameObject);
    }
}
