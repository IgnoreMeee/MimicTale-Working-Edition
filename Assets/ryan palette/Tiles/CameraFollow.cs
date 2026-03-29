using UnityEngine;

//hey guys its ryan tang i put some notes so you can understand what i learnt !!!!!
public class CameraFollow : MonoBehaviour
{

    public PlayerCharacter player;
    public float speed = 0.2f;
    public Vector3 offset;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset.z = -10f;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void LateUpdate() 
    /*exists because player position is updated using void update, so 
    including camera follow there will result in the two competing for who goes first,
    resulting in jittery movement*/
    {
        Vector3 desiredPosition = player.playerPos + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed); 
        //Lerp stands for linear interpolation, essentially the process of smoothly going from point a to b
        /*speed represents variable t in lerp, for better understanding, if speed was equal to 1, we would
        instantly snap to desired position, if speed was 0, nothing would happen at all. a number
        between 0 and 1 allows for smooth movement*/

        transform.position = smoothedPosition;
    }
}
