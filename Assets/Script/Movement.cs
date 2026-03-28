using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    [SerializeField] Rigidbody2D rb;
    public GameObject dialogbox;

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); 
        float moveY = Input.GetAxis("Vertical");   

        rb.linearVelocity = new Vector2(moveX, moveY) * speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("talkbox") && Input.GetKeyDown(KeyCode.E))
        {
            dialogbox.SetActive(true);
        }
    }

}
