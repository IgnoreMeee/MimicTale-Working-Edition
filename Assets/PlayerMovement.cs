using UnityEngine;

public class PlayerCharacter : MonoBehaviour

    {
    int prevSecond = 0;
    bool isMovingX = false;
    bool isMovingY = false;

    float playerVelX = 0f;
    float playerVelY = 0f;
    float playerVel = 5f; // units per second
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Move();
    }

    void Move()
    {
        transform.position += new Vector3(playerVelX, playerVelY, 0f) * Time.deltaTime;
    }


    void PlayerMovement()
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

    public float GetPlayerX()
    {
        return transform.position.x;
    }

    public float GetPlayerY()
    {
        return transform.position.y;
    }
}
