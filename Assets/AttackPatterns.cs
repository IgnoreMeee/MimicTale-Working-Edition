using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPatterns : MonoBehaviour
{
    BattleManager bm;
    SoulMovement sm;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    public bool hitPlayer = false;
    GameObject[] leftBullets = new GameObject[10];
    GameObject[] rightBullets = new GameObject[10];

    bool[] moveLeftSet = new bool[10];
    bool[] moveRightSet = new bool[10];
    bool attacking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bm = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        sm = GameObject.Find("Soul").GetComponent<SoulMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        moveLeftBullets();
        moveRightBullets();

        if (bm.isPlayerTurn)
        {
            deleteBullets();
        }

        if (!attacking && !bm.isPlayerTurn) {
            int attackChoice = Random.Range(0, 2);
            if (attackChoice == 0)
                StartCoroutine(Attack1(1f));
            else
            StartCoroutine(Attack2(1f));

            attacking = true;
            Debug.Log("Enemy Attacking with pattern " + attackChoice);
        }

        
    }

    void moveLeftBullets() {
        for (int i = 0; i < leftBullets.Length; i++)
        {
            GameObject bullet = leftBullets[i];
            if (bullet != null)
            {
                if (moveLeftSet[i])
                {
                    bullet.transform.position += new Vector3(0.1f, 0f, 0f);
                }

            }
        }
    }

    void moveRightBullets() {
        for (int i = 0; i < rightBullets.Length; i++)
        {
            GameObject bullet = rightBullets[i];
            if (bullet != null)
            {
                if (moveRightSet[i])
                {
                    bullet.transform.position += new Vector3(-0.1f, 0f, 0f);
                }

            }
        }
    }

    public IEnumerator Attack1(float delay)
    {
        for (int j = 0; j < 3; j ++) {
        int startIndex = j * 3;
        int endIndexExclusive = startIndex + 3;
        for (int i = 0; i < 3; i++)
        {
            int bulletIndex = startIndex + i;
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(bulletPrefab.transform.position.x + 15, bulletPrefab.transform.position.y - (i * 2f) + Random.Range(-1f, 2f), 0), Quaternion.identity);
            bullet.SetActive(true);
            rightBullets[bulletIndex] = bullet;
        }

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < 3; i++)
        {
            int bulletIndex = startIndex + i;
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(bulletPrefab.transform.position.x + 2, bulletPrefab.transform.position.y - (i * 2f) + Random.Range(-1f, 2f), 0), Quaternion.identity);
            bullet.SetActive(true);
            leftBullets[bulletIndex] = bullet;
        }

        yield return new WaitForSeconds(delay - 0.95f);
        for (int i = startIndex; i < endIndexExclusive; i++) moveRightSet[i] = true;
        yield return new WaitForSeconds(delay - 0.95f);
        for (int i = startIndex; i < endIndexExclusive; i++) moveLeftSet[i] = true;
        yield return new WaitForSeconds(delay);

        for (int i = startIndex; i < endIndexExclusive; i++) moveLeftSet[i] = false;
        for (int i = startIndex; i < endIndexExclusive; i++) moveRightSet[i] = false;

        for (int i = startIndex; i < endIndexExclusive; i++)
        {
            if (leftBullets[i] != null)
            {
                Destroy(leftBullets[i]);
                leftBullets[i] = null;
            }

            if (rightBullets[i] != null)
            {
                Destroy(rightBullets[i]);
                rightBullets[i] = null;
            }
        }
        }

        bm.isPlayerTurn = true;
        attacking = false;

    }

    public IEnumerator Attack2(float delay) {
            float leftSpawnX = -6.0f;
            float rightSpawnX = 6.0f;
            float minY = -2.25f;
            float maxY = 0.75f;
            float appearDelay = 0.35f;
            float moveDelay = 0.25f;

            for (int i = 0; i < leftBullets.Length; i++)
            {
                if (leftBullets[i] != null) Destroy(leftBullets[i]);
                if (rightBullets[i] != null) Destroy(rightBullets[i]);
                moveLeftSet[i] = false;
                moveRightSet[i] = false;
            }

            for (int set = 0; set < leftBullets.Length; set++)
            {
                float laneY = Random.Range(minY, maxY);

                GameObject leftBullet = Instantiate(bulletPrefab, new Vector3(leftSpawnX, laneY, 0f), Quaternion.identity);
                GameObject rightBullet = Instantiate(bulletPrefab, new Vector3(rightSpawnX, laneY, 0f), Quaternion.identity);
                leftBullet.SetActive(true);
                rightBullet.SetActive(true);

                leftBullets[set] = leftBullet;
                rightBullets[set] = rightBullet;

                if (set == 0)
                {
                    yield return new WaitForSeconds(moveDelay);
                }
                else
                {
                    yield return new WaitForSeconds(appearDelay);
                    yield return new WaitForSeconds(moveDelay);
                }

                moveLeftSet[set] = true;
                moveRightSet[set] = true;
            }

            yield return new WaitForSeconds(delay - 0.75f);

            for (int i = 0; i < moveLeftSet.Length; i++)
            {
                moveLeftSet[i] = false;
                moveRightSet[i] = false;
            }

            bm.isPlayerTurn = true;
            attacking = false;

        }

    void deleteBullets() {
        for (int i = 0; i < leftBullets.Length; i++)
        {
            moveLeftSet[i] = false;
            if (leftBullets[i] != null)
            {
                Destroy(leftBullets[i]);
                leftBullets[i] = null;
            }
        }

        for (int i = 0; i < rightBullets.Length; i++)
        {
            moveRightSet[i] = false;
            if (rightBullets[i] != null)
            {
                Destroy(rightBullets[i]);
                rightBullets[i] = null;
            }
        }
    }
}
