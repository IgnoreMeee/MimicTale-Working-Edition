using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    [SerializeField] Rigidbody2D rb;
    public GameObject canva;

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D + ← →
        float moveY = Input.GetAxis("Vertical");   // W/S + ↑ ↓

        rb.linearVelocity = new Vector2(moveX, moveY) * speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("talkbox"))
        {
            canva.SetActive(true);
        }
    }
}
