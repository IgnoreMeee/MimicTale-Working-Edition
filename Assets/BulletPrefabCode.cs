using UnityEngine;

public class BulletPrefabCode : MonoBehaviour
{
    AttackPatterns ap;
    SoulMovement sm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ap = GameObject.Find("AttackPatterns").GetComponent<AttackPatterns>();
        sm = GameObject.Find("Soul").GetComponent<SoulMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sm.hp -= 2;
            Debug.Log("Player hit! HP: " + sm.hp);
            Destroy(gameObject);
        }
    }
}
