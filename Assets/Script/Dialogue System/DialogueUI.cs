using System.Collections; 
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;
    [SerializeField] private TMP_Text _textLabel;
    public GameObject dialoguebox;

    private TypeWriting typewriterEffect;
    private bool _isTalking = false;


 
    void Awake()
    {
        Instance = this;
        typewriterEffect = GetComponent<TypeWriting>();
    }

    public void StartDialogue(DialogueObject dialogueObject)
    {
        if (_isTalking) return;
        StartCoroutine(StepThroughDialogue(dialogueObject));//starts dialogue
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        _isTalking = true;
        dialoguebox.SetActive(true);

        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, _textLabel); //animation
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        }
        _isTalking = false;
        dialoguebox.SetActive(false);
        
    }

    public bool IsTalking() //getter
    {
        return _isTalking;
    }
}
