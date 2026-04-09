using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    GameObject topRect;
    GameObject bottomRect;
    GameObject leftRect;
    GameObject rightRect;
    Inventorycontroller ic = Inventorycontroller.Instance;
    AttackPatterns ap;

    bool loadedFightText = false;
    bool loadedItemText = false;


    public GameObject textPrefab;

    SoulMovement sm;

    public string[] menuOptions = new string[] {"Fight", "Item"};
    public string[] bigMenuOptions = new string[] {"Dummy 1", "Dummy 2", "Dummy 3", "Dummy 4"}; 
    string playerMenuOption;
    public int menuIndex = 0;
    public int bigMenuIndex = 0;
    public bool bigMenuOpen = false;
    public bool isPlayerTurn = true;

    bool enterLock = false;
    string[] enemyList = new string[] {"Dummy 1", "Dummy 2"};
    string[] itemList = new string[] {"Pizza", "Stick"};
    
    Dictionary<string, int> enemyHealth = new Dictionary<string, int>();
    Dictionary<GameObject, string> enemyObjects = new Dictionary<GameObject, string>();
    Dictionary<GameObject, string> itemObjects = new Dictionary<GameObject, string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topRect = GameObject.Find("topRect");
        bottomRect = GameObject.Find("bottomRect");
        leftRect = GameObject.Find("leftRect");
        rightRect = GameObject.Find("rightRect");
        sm = GameObject.Find("Soul").GetComponent<SoulMovement>();
        ic = Inventorycontroller.Instance;
        ap = GameObject.Find("AttackPatterns").GetComponent<AttackPatterns>();

        foreach (string enemy in enemyList)
        {
            enemyHealth[enemy] = 100; // example health value for each enemy
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (string item in itemList) {
            if (item == "Pizza") {
                if (ic.pizza <= 0) {
                    // remove pizza from item menu if player has 0 pizza
                    itemList = Array.FindAll(itemList, i => i != "Pizza");
                } 
            else if (item == "Stick") {
                    if (ic.stick <= 0) {
                        // remove stick from item menu if player has 0 stick
                        itemList = Array.FindAll(itemList, i => i != "Stick");
                    }
                }
            }
        }

        if (ic.pizza > 0 && !Array.Exists(itemList, item => item == "Pizza")) {
            // add pizza back to item menu if player has more than 0 pizza
            Array.Resize(ref itemList, itemList.Length + 1);
            itemList[itemList.Length - 1] = "Pizza";
        } else if (ic.stick > 0 && !Array.Exists(itemList, item => item == "Stick")) {
            // add stick back to item menu if player has more than 0 stick
            Array.Resize(ref itemList, itemList.Length + 1);
            itemList[itemList.Length - 1] = "Stick";
        }

        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }

        if (enterLock && Input.GetKeyUp(KeyCode.Return))
        {
            enterLock = false;
        }
    }

    void PlayerTurn()
    {
        // player logic

        // topRect.SetActive(false);
        // bottomRect.SetActive(false);
        // leftRect.SetActive(false);
        // rightRect.SetActive(false);

        // 0.15f, 1.85f, 0f
        // 10.16793f, 0.3060362f, 1f

        // 0.13f, -1.84f, 0f
        // 10.16793f, 0.3060362f, 1

        // -4.83f, 0.009999946f, 0f
        // 3.983608f, 0.3060362f, 1

        // 5.07f, -0.02000005f, 0f
        // 3.983608f, 0.3060362f, 1

        topRect.transform.position = new Vector3(0.16f, 0.13f, 0f); //0.22f, 0.95f, 0f
        topRect.transform.localScale = new Vector3(12.8116f, 0.1928028f, 1f); //5.877419f, 0.3060362f, 1f

        bottomRect.transform.position = new Vector3(0.14f, -2.62f, 0f); //0.25f, -4.03f, 0f
        bottomRect.transform.localScale = new Vector3(12.70991f, 0.1928028f, 1f); //5.877419f, 0.3060362f, 1f

        leftRect.transform.position = new Vector3(-6.14f, -1.24f, 0f); //-2.59f, -1.55f, 0f
        leftRect.transform.localScale = new Vector3(2.923267f, 0.1928028f, 1f); //5.24159f, 0.3060362f, 1f

        rightRect.transform.position = new Vector3(6.51f, -1.25f, 0f); //3.04f, -1.55f, 0f
        rightRect.transform.localScale = new Vector3(2.921501f, 0.1928028f, 1f); //5.24159f, 0.3060362f, 1f


        playerMenuOption = menuOptions[menuIndex];

        PlayerMenu();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switchToEnemyTurn();
            return;
        }

        if (!bigMenuOpen)
        {
            MenuSwitch();
        }

        if (Input.GetKeyDown(KeyCode.Return) && !enterLock)
        {
            enterLock = true;
            if (!bigMenuOpen) {
                bigMenuOpen = true;
                BigMenu();
                return;
            }

            if (playerMenuOption == "Fight")
            {
                AttackEnemy(enemyList[bigMenuIndex]);
                bigMenuOpen = false;
                return;
            } else if (playerMenuOption == "Item")
            {
                UseItem();
                bigMenuOpen = false;
                return;
             }
        }

        if (bigMenuOpen && Input.GetKeyDown(KeyCode.RightShift)) {
            bigMenuOpen = false;
        }

    }

    void EnemyTurn()
    {
        
        // enemy logic
        topRect.SetActive(true);
        bottomRect.SetActive(true);
        leftRect.SetActive(true);
        rightRect.SetActive(true);

        // topRect.transform.position = new Vector3(0.22f, 0.95f, 0f); //0.22f, 0.95f, 0f
        // topRect.transform.localScale = new Vector3(5.877419f, 0.3060362f, 1f); //5.877419f, 0.3060362f, 1f

        // bottomRect.transform.position = new Vector3(0.25f, -4.03f, 0f); //0.25f, -4.03f, 0f
        // bottomRect.transform.localScale = new Vector3(5.877419f, 0.3060362f, 1f); //5.877419f, 0.3060362f, 1f

        // leftRect.transform.position = new Vector3(-2.59f, -1.55f, 0f); //-2.59f, -1.55f, 0f
        // leftRect.transform.localScale = new Vector3(5.24159f, 0.3060362f, 1f); //5.24159f, 0.3060362f, 1f

        // rightRect.transform.position = new Vector3(3.04f, -1.55f, 0f); //3.04f, -1.55f, 0f
        // rightRect.transform.localScale = new Vector3(5.24159f, 0.3060362f, 1f); //5.24159f, 0.3060362f, 1f

       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlayerTurn = true;
            menuIndex = 0;
        }


    }

    void PlayerMenu() {
        if (!bigMenuOpen) {
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
        } else
        {
            switch (bigMenuIndex)
            {
                case 3:
                    sm.rb.position = new Vector3(-5.54f, -1.93f, 0f);
                    break;
                case 2:
                    sm.rb.position = new Vector3(-5.54f, -1.43f, 0f);
                    break;
                case 1:
                    sm.rb.position = new Vector3(-5.54f, -0.93f, 0f);
                    break;
                case 0:
                    sm.rb.position = new Vector3(-5.54f, -0.43f, 0f);
                    // Debug.Log(bigMenuReturn);
                    break;
                    
            }
        }
    }

    void loadFightMenu()
    {
        if (!loadedFightText) {
        for (int i = 0; i < enemyList.Length; i++)
        {
            // create enemy buttons based on enemyList
            GameObject enemyButton = Instantiate(textPrefab, new Vector3(textPrefab.transform.position.x - 110, (textPrefab.transform.position.y - 20) - i * 20, 0f), Quaternion.identity);
            enemyButton.transform.SetParent(GameObject.Find("Canvas").transform, false);


            enemyObjects[enemyButton] = enemyList[i];
            TMP_Text text = enemyButton.GetComponent<TMP_Text>();
            text.text = enemyList[i] + " HP: " + enemyHealth[enemyList[i]];
            text.fontSize = 8;


            enemyButton.SetActive(true);

        }
        bigMenuOptions = enemyList;
        loadedFightText = true;
     }
    }

    void unloadFightMenu()
    {
        foreach (GameObject enemyButton in enemyObjects.Keys)
        {
            Destroy(enemyButton);
        }
        enemyObjects.Clear();
        loadedFightText = false;
    }

    void loadItemMenu()
    {
        if (!loadedItemText)
        {
            for (int i = 0; i < itemList.Length; i++)
            {
                // create item buttons based on itemList
                GameObject itemButton = Instantiate(textPrefab, new Vector3(textPrefab.transform.position.x - 110, (textPrefab.transform.position.y - 20) - i * 20, 0f), Quaternion.identity);
                itemButton.transform.SetParent(GameObject.Find("Canvas").transform, false);

                itemObjects[itemButton] = itemList[i];
                TMP_Text text = itemButton.GetComponent<TMP_Text>();

                if (itemList[i] == "Pizza")
                    text.text = itemList[i] + " x" + ic.pizza;
                else if (itemList[i] == "Stick")
                    text.text = itemList[i] + " x" + ic.stick;
                else
                    text.text = itemList[i];

                itemButton.SetActive(true);
            }

            loadedItemText = true;
        }
    }

    void unloadItemMenu()
    {
        foreach (GameObject itemButton in itemObjects.Keys)
        {
            Destroy(itemButton);
        }
        itemObjects.Clear();
        loadedItemText = false;
    }

    void MenuSwitch() {
        if (playerMenuOption == "Fight")
            {
                unloadItemMenu();
                loadFightMenu();
            }
            else if (playerMenuOption == "Act")
            {
                unloadFightMenu();
                unloadItemMenu();
            }
            else if (playerMenuOption == "Item")
            {
                unloadFightMenu(); 
                loadItemMenu();
            }
            else if (playerMenuOption == "Mercy")
            {
                unloadFightMenu();
                unloadItemMenu();
                
            }
    }

    void BigMenu() {
        sm.rb.position = new Vector3(-5.54f, -0.43f, 0f);
        Debug.Log("Big menu opened");
    }
    
    void AttackEnemy(string enemy) {
        // example attack logic
        if (enemyHealth.ContainsKey(enemy))
        {
            enemyHealth[enemy] -= 20; 
            Debug.Log(enemy + " took damage! Remaining health: " + enemyHealth[enemy]);
        }
        switchToEnemyTurn();

        bool allDead = true;
        foreach (int hp in enemyHealth.Values)
        {
            if (hp > 0)
            {
                allDead = false;
                break;
            }
        }

        if (allDead)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    void switchToEnemyTurn()
    {
        isPlayerTurn = false;
        bigMenuOpen = false;

        topRect.transform.position = new Vector3(0.22f, 0.95f, 0f); //0.22f, 0.95f, 0f
        topRect.transform.localScale = new Vector3(5.877419f, 0.3060362f, 1f); //5.877419f, 0.3060362f, 1f

        bottomRect.transform.position = new Vector3(0.25f, -4.03f, 0f); //0.25f, -4.03f, 0f
        bottomRect.transform.localScale = new Vector3(5.877419f, 0.3060362f, 1f); //5.877419f, 0.3060362f, 1f

        leftRect.transform.position = new Vector3(-2.59f, -1.55f, 0f); //-2.59f, -1.55f, 0f
        leftRect.transform.localScale = new Vector3(5.24159f, 0.3060362f, 1f); //5.24159f, 0.3060362f, 1f

        rightRect.transform.position = new Vector3(3.04f, -1.55f, 0f); //3.04f, -1.55f, 0f
        rightRect.transform.localScale = new Vector3(5.24159f, 0.3060362f, 1f); //5.24159f, 0.3060362f, 1f

        sm.rb.position = new Vector3(0.19f, -1.75f, 0f);

        unloadFightMenu();
        unloadItemMenu();



    }

    void UseItem() {
        if (itemList[bigMenuIndex] == "Pizza") {
            if (ic.pizza > 0) {
                ic.pizza -= 1;
                sm.hp += 2;
                Debug.Log("Used Pizza! HP: " + sm.hp);
            } else {
                Debug.Log("No Pizza left!");
            }
            switchToEnemyTurn();
        }else {
            Debug.Log("Item cannot be used right now!!");
        }
    }
}
