using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Silhouette", menuName ="Create new silhouette")]
public class GuessBase : ScriptableObject
{
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite silhouette;
    [SerializeField] private Sprite[] incorrectAnswers = new Sprite[3];
    


    public Sprite Normal => normal;
    public Sprite Silhouette => silhouette;
    public Sprite CorrectAnswer => normal;
    public Sprite[] IncorrectAnswers => incorrectAnswers;


}
