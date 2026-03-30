using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] private float holdMoveDelay = 0.14f;
    [SerializeField] private float moveLerpSpeed = 12f;
    private float moveCooldown = 0f;
    private Vector3Int heldDirection = Vector3Int.zero;
    private bool isMoving = false;
    private Vector3 moveStart;
    private Vector3 moveTarget;
    private float moveLerpT = 0f;
    [SerializeField] private Tilemap tilemap;
    private Vector3Int currentCell;

    void Start()
    {
        if (tilemap == null)
            tilemap = FindAnyObjectByType<Tilemap>();

        if (tilemap == null)
        {
            Debug.LogError("Player could not find a Tilemap in the scene. Assign one in the Inspector.", this);
            enabled = false;
            return;
        }

        currentCell = tilemap.WorldToCell(transform.position);
    }
    

    void Update()
    {
        if (moveCooldown > 0f)
            moveCooldown -= Time.deltaTime;

        UpdateLerpMovement();

        if (!isMoving)
            MovePlayer();
    }

    void Move(Vector3Int direction)
    {
        currentCell += direction;
        moveStart = transform.position;
        moveTarget = tilemap.GetCellCenterWorld(currentCell);
        moveLerpT = 0f;
        isMoving = true;
    }

    void UpdateLerpMovement()
    {
        if (!isMoving)
            return;

        moveLerpT += Time.deltaTime * moveLerpSpeed;
        transform.position = Vector3.Lerp(moveStart, moveTarget, moveLerpT);

        if (moveLerpT >= 1f)
        {
            transform.position = moveTarget;
            isMoving = false;
        }
    }

    void MovePlayer()
    {
        Vector3Int direction = Vector3Int.zero;

        if (Input.GetKey(KeyCode.W))
            direction = Vector3Int.up;
        else if (Input.GetKey(KeyCode.A))
            direction = Vector3Int.left;
        else if (Input.GetKey(KeyCode.S))
            direction = Vector3Int.down;
        else if (Input.GetKey(KeyCode.D))
            direction = Vector3Int.right;

        if (direction == Vector3Int.zero)
        {
            heldDirection = Vector3Int.zero;
            moveCooldown = 0f;
            return;
        }

        if (direction != heldDirection)
        {
            heldDirection = direction;
            Move(direction);
            moveCooldown = holdMoveDelay;
            return;
        }

        if (moveCooldown > 0f)
            return;

        Move(direction);
        moveCooldown = holdMoveDelay;
    }
}
