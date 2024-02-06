using UnityEngine;


[CreateAssetMenu(fileName = "MakeMeme", menuName = "Create new Phrase")]
public class PhraseData : ScriptableObject
{
    [SerializeField] Sprite memeImage;
    [SerializeField] string originalPhrase;
    [SerializeField] string incompletePhrase;
    [SerializeField] string correctWord;
    [SerializeField] string[] incorrectWords;

    public string ChooseCorrect=> correctWord;
    public string OriginalPhrase => originalPhrase;
    public string[] IncorrectWords => incorrectWords;
    public string IncompletePhrase => incompletePhrase;
    public Sprite MemeImage => memeImage;
}
