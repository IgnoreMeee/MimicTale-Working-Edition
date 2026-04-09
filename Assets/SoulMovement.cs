using UnityEngine;
using UnityEngine.InputSystem;

public class SoulMovement : MonoBehaviour
{
    BattleManager bm;

    float playerVelX = 0f;
    float playerVelY = 0f;
    public float playerVel = 5f;
    public int hp = 20;
    Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float moveSpeed = 7f;
    public Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    void Update()
    {
        if (bm.isPlayerTurn)
        {
            PlayerTurnMovement();
        } else {
            EnemyTurnMovement();
        }

        // Debug.Log("X:" + rb.position.x + " Y:" + rb.position.y);
    }

    void FixedUpdate()
    {
        Move();
    }


    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void EnemyTurnMovement()
    {
        playerVelX = 0f;
        playerVelY = 0f;

        if (Input.GetKey(KeyCode.W))
            playerVelY += playerVel;

         if (Input.GetKey(KeyCode.S))
                playerVelY += -playerVel;
                

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) 
            playerVelY = 0f;

        if (Input.GetKey(KeyCode.A))
                playerVelX += -playerVel;

        if (Input.GetKey(KeyCode.D))
                playerVelX += playerVel;

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            playerVelX = 0f;
    }

    void PlayerTurnMovement()
    {
        playerVelX = 0f;
        playerVelY = 0f;

        if (!bm.bigMenuOpen) {

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (bm.menuIndex < bm.menuOptions.Length - 1) 
            bm.menuIndex++;
            else
            {
                bm.menuIndex = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (bm.menuIndex > 0) 
            bm.menuIndex--;
            else
            {
                bm.menuIndex = bm.menuOptions.Length - 1;
            }
        }
        } else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (bm.bigMenuIndex > 0) 
                bm.bigMenuIndex--;
                else
                {
                    bm.bigMenuIndex = bm.bigMenuOptions.Length - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (bm.bigMenuIndex < bm.bigMenuOptions.Length - 1) 
                bm.bigMenuIndex++;
                else
                {
                    bm.bigMenuIndex = 0;
                }
        }
    }
    }

    void Move()
    {
        movement = new Vector2(playerVelX, playerVelY);
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

    }

    void MoveToBigMenu()
    {
        rb.position = new Vector3(-0.01f, -3.61f, 0f);
    }
}


