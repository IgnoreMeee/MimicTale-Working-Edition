using System.Collections; 
using UnityEngine;
using TMPro;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    public GameObject dialoguebox;


    private TypeWriting typewriterEffect;

 
    private void Start()
    {
        typewriterEffect = GetComponent<TypeWriting>();
        ShowDialogue(testDialogue);
    }

    private void ShowDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        }
        dialoguebox.SetActive(false);
        

    }

}
