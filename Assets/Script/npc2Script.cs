using UnityEngine;

public class npc2Script : MonoBehaviour
{
    [SerializeField] private DialogueObject _dialogueContent;
    public GameObject talk;
    [SerializeField] float interactRange = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //enable dialogue!!!
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= interactRange)
        {
            talk.SetActive(true); 

            if (Input.GetKeyDown(KeyCode.F))
            {
                player.position = new Vector3(transform.position.x - 3, transform.position.y, 0f);
                DialogueUI.Instance.StartDialogue(_dialogueContent, true);
            }
        }
        else
        {
            talk.SetActive(false);
        }
    }
    
}
