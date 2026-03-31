using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Rigidbody2D rb;
    

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); 
        float moveY = Input.GetAxis("Vertical");   

        rb.linearVelocity = new Vector2(moveX, moveY) * speed;
    }


//if player is in a specific range close to the talkbox, [press E to talk] appear, if the player press E, start talking

}
