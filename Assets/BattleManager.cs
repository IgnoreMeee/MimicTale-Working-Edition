using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject topRect;
    GameObject bottomRect;
    GameObject leftRect;
    GameObject rightRect;

    SoulMovement sm;

    public string[] menuOptions = new string[] {"Fight", "Act", "Item", "Mercy"};
    public string playerMenuOption;
    public int menuIndex = 0;

    public bool isPlayerTurn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topRect = GameObject.Find("topRect");
        bottomRect = GameObject.Find("bottomRect");
        leftRect = GameObject.Find("leftRect");
        rightRect = GameObject.Find("rightRect");
        sm = GameObject.Find("Soul").GetComponent<SoulMovement>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

    void PlayerTurn()
    {
        // player logic
        topRect.SetActive(false);
        bottomRect.SetActive(false);
        leftRect.SetActive(false);
        rightRect.SetActive(false);

        playerMenuOption = menuOptions[menuIndex];

        PlayerMenu();

    }

    void EnemyTurn()
    {
        // enemy logic
        topRect.SetActive(true);
        bottomRect.SetActive(true);
        leftRect.SetActive(true);
        rightRect.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlayerTurn = true;
            menuIndex = 0;
        }
    }

    void PlayerMenu() {
        switch (playerMenuOption)
        {
            case "Fight":
                sm.rb.position = new Vector3(-8.78f, -3.61f, 0f);
                break;
            case "Act":
                sm.rb.position = new Vector3(-3.78f, -3.61f, 0f);
                break;
            case "Item":
                sm.rb.position = new Vector3(2.78f, -3.61f, 0f);
                break;
            case "Mercy":
                sm.rb.position = new Vector3(7.78f, -3.61f, 0f);
                break;
        }
    }
}
