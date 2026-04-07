using System.Collections; 
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [SerializeField] private TMP_Text _textLabel;
    [SerializeField] private GameObject dialoguebox;
    [SerializeField] private Image portraitImage;

    private TypeWriting typewriterEffect;
    private bool _isTalking = false;


 
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        typewriterEffect = GetComponent<TypeWriting>();
    }

    public void StartDialogue(DialogueObject dialogueObject, bool isMonster)
    {
        if (_isTalking) return;
        
        portraitImage.sprite = dialogueObject.portrait;

        StartCoroutine(StepThroughDialogue(dialogueObject, isMonster));//starts dialogue
    }

    //for normal conversation
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject, bool isMonster)
    {
        _isTalking = true;
        dialoguebox.SetActive(true);

        foreach (string dialogue in dialogueObject.Dialogue)
        {
            Debug.Log("Line: " + dialogue);
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
