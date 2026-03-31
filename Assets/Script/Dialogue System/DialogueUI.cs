using System.Collections; 
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    public void StartDialogue(DialogueObject dialogueObject, bool isMonster)
    {
        if (_isTalking) return;
        
       
        StartCoroutine(StepThroughDialogue(dialogueObject, isMonster));//starts dialogue
    }

    //for normal conversation
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject, bool isMonster)
    {
        _isTalking = true;
        dialoguebox.SetActive(true);

        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, _textLabel); //animation
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        }
        _isTalking = false;
        
        if (isMonster)
        {
            dialoguebox.SetActive(false);
            SceneManager.LoadScene("SampleScene");
            
        } else {dialoguebox.SetActive(false);}
        
        
    }

    public bool IsTalking() //getter
    {
        return _isTalking;
    }
}
