using UnityEngine;

public class npc1Script : MonoBehaviour
{
    [SerializeField] private DialogueObject _dialogueContent;
    public GameObject talk;
    [SerializeField] float interactRange = 2f;
    [SerializeField] Transform player;


    void Start()
    {
    }

    void Update()
    {
        //enable dialogue!!!
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= interactRange)
        {
            //talk.SetActive(true); 

            if (Input.GetKeyDown(KeyCode.F))
            {
                //player.position = new Vector3(transform.position.x - 3, transform.position.y, 0f);
                DialogueUI.Instance.StartDialogue(_dialogueContent, false);
            }
        }
        //else
        //{
        //     talk.SetActive(false);
        //}
    }
    
}
