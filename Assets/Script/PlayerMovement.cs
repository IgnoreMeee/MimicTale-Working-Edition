using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCharacter : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap collidables;
    public Tilemap collectables;
    public Tilemap stones;
    public TileBase stick;
    public TileBase pizza;

    float collisionUp;
    float collisionDown;
    float collisionRight;
    float collisionLeft;

    int prevSecond = 0;
    bool isMovingX = false;
    bool isMovingY = false;

    float playerVelX = 0f;
    float playerVelY = 0f;
    float playerVel = 5f;

    float playerHalfWidth = 0.45f;
    float playerHalfHeight = 0.45f;

    public Vector3 playerPos;
    Vector3Int currentCell;
    
    Vector3Int facingDir = Vector3Int.down;


    int getWeapon = 1;



    //Collectable Events
    public event Action addPizzaevent;
    public event Action addStickevent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerTilePos();
        Collisions();
        // GetItemPickup();
        // StoneCollision();
        HandleInteraction();

        FixCollisions();
        PlayerMovement();
        Move();

        playerPos = transform.position;


        // identify which direction player is facing
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX > 0)
        {
            facingDir = Vector3Int.right;
        } else if (moveX < 0){
            facingDir = Vector3Int.left;
        } else if (moveY > 0){
            facingDir = Vector3Int.up;
        } else if (moveY < 0){
            facingDir = Vector3Int.down;
        }
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
            if (transform.position.y < collisionUp) {
            playerVelY += playerVel;}

         if (Input.GetKey(KeyCode.S))
            if (transform.position.y > collisionDown) {
                playerVelY += -playerVel;}

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
            playerVelY = 0f;}

        if (Input.GetKey(KeyCode.A))
            if (transform.position.x > collisionLeft) {
                playerVelX += -playerVel;}

        if (Input.GetKey(KeyCode.D))
            if (transform.position.x < collisionRight) {
                playerVelX += playerVel;}

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

    public void PlayerTilePos()
    {
        currentCell = tilemap.WorldToCell(transform.position);
        // Debug.Log(currentCell);
    }

    public void Collisions()
    {
        Vector3 scaledCellSize = Vector3.Scale(tilemap.cellSize, tilemap.transform.lossyScale);
        
        float halfCellWidth = Mathf.Abs(scaledCellSize.x) * 0.5f;
        float halfCellHeight = Mathf.Abs(scaledCellSize.y) * 0.5f;

        Vector3Int upCell = currentCell + Vector3Int.up;
        Vector3Int downCell = currentCell + Vector3Int.down;
        Vector3Int rightCell = currentCell + Vector3Int.right;
        Vector3Int leftCell = currentCell + Vector3Int.left;

        if (collidables.GetTile(upCell) != null || collectables.GetTile(upCell) != null || stones.GetTile(upCell) != null) {
            float blockedTileBottom = tilemap.GetCellCenterWorld(upCell).y - halfCellHeight;
            collisionUp = blockedTileBottom - playerHalfHeight;
        } else {
            collisionUp = float.PositiveInfinity;
        }

        if (collidables.GetTile(downCell) != null || collectables.GetTile(downCell) != null || stones.GetTile(downCell) != null) {
            float blockedTileTop = tilemap.GetCellCenterWorld(downCell).y + halfCellHeight;
            collisionDown = blockedTileTop + playerHalfHeight;
        } else {
            collisionDown = float.NegativeInfinity;
        }

        if (collidables.GetTile(rightCell) != null || collectables.GetTile(rightCell) != null || stones.GetTile(rightCell) != null) {
            float blockedTileLeft = tilemap.GetCellCenterWorld(rightCell).x - halfCellWidth;
            collisionRight = blockedTileLeft - playerHalfWidth;
        } else {
            collisionRight = float.PositiveInfinity;
        }

        if (collidables.GetTile(leftCell) != null || collectables.GetTile(leftCell) != null || stones.GetTile(leftCell) != null) {
            float blockedTileRight = tilemap.GetCellCenterWorld(leftCell).x + halfCellWidth;
            collisionLeft = blockedTileRight + playerHalfWidth;
        } else {
            collisionLeft = float.NegativeInfinity;
        }


    }

    void FixCollisions()
    {
        if (transform.position.y > collisionUp) {
            transform.position = new Vector3(transform.position.x, collisionUp, transform.position.z);
        }

        if (transform.position.y < collisionDown) {
            transform.position = new Vector3(transform.position.x, collisionDown, transform.position.z);
        }

        if (transform.position.x > collisionRight) {
            transform.position = new Vector3(collisionRight, transform.position.y, transform.position.z);
        }

        if (transform.position.x < collisionLeft) {
            transform.position = new Vector3(collisionLeft, transform.position.y, transform.position.z);
        }
    }


void HandleInteraction()
{
    if (!Input.GetKeyDown(KeyCode.F)) return;

    Vector3Int targetCell = currentCell + facingDir;
    TileBase tile = collectables.GetTile(targetCell);

    if (tile != null )
    {
        if (tile == stick)
            {
                addStickevent.Invoke();
                Debug.Log("You got a stick!");
            }
        if (tile == pizza)
            {
                {
                addPizzaevent.Invoke();
                Debug.Log("You got a pizza!");
                }
            }
        collectables.SetTile(targetCell, null);
        return;
    }

    if (stones.GetTile(targetCell) != null)
    {
        PushStone(facingDir);
    }
}


void PushStone(Vector3Int dir)
{

    Vector3Int stonePos = currentCell + dir;
    // getting the new location
    Vector3Int targetPos = stonePos + dir;

     if (stones.GetTile(stonePos) == null) return;

    // no override allowed!!!!
    if (stones.GetTile(targetPos) != null) return;
    if (collidables.GetTile(targetPos) != null) return;

    //push
    TileBase stone = stones.GetTile(stonePos);
    stones.SetTile(stonePos, null);
    stones.SetTile(targetPos, stone);
}

}
