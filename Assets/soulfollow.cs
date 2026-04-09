using UnityEngine;

public class soulfollow : MonoBehaviour
{

    public GameObject soul;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = soul.transform.position;
    }
}
