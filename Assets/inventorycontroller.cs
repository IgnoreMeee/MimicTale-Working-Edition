using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

public class inventorycontroller : MonoBehaviour
{
    // Pairing with Items
    public TextMeshProUGUI item1;
    public TextMeshProUGUI item2;
    public TextMeshProUGUI item3;
    public TextMeshProUGUI item4;
    public TextMeshProUGUI item5;
    public TextMeshProUGUI item6;
    public TextMeshProUGUI item7;
    public TextMeshProUGUI item8;
    public TextMeshProUGUI item9;
    public TextMeshProUGUI item10;
    public GameObject inventorycanvas;

    //item counts
    int item1count;
    int item2count;
    int item3count;
    int item4count;
    int item5count;
    int item6count;
    int item7count;
    int item8count;
    int item9count;
    int item10count;

    bool isActive;

    //Collectable Initialization
    public int stick;
    public int pizza;
    public int key;

    [SerializeField] private PlayerCharacter Sender;
    public string[] Items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Items = new string[] {};
        //Items = Items.Append("ball").ToArray();
        //Debug.Log(Items[0]);
        
        Sender.addStickevent += addStick;
        Sender.addPizzaevent += addPizza;
        Sender.addKeyevent += addKey;

        if (key == 2)
        {
            Debug.Log("door open");
        } 
       
        
    
    }   

    // Update is called once per frame
    void Update()
    {
        checkItem1();
        checkItem2();
        checkItem3();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(isActive == true)
            {
                inventorycanvas.SetActive(false);
                isActive = false;
            }
            else    
            {
                inventorycanvas.SetActive(true);
                isActive = true;
            }
            
        }
    }

    public void addStick()
    {
        if (Items.Contains("stick"))
        {

        }
        else //if stick doesnt exist in inventory, add it
        {
            Items = Items.Append("stick").ToArray();
        }
        stick += 1;
        
    }

    public void addPizza()
    {
        if (Items.Contains("pizza"))
        {

        }
        else 
        {
            Items = Items.Append("pizza").ToArray();
        }
        pizza += 1;
        
    }

    public void addKey()
    {
        if (Items.Contains("key"))
        {

        }
        else
        {
            Items = Items.Append("key").ToArray();
        }
        key += 1;
    }

    void checkItem1()
    {
        if(Items.Length > 0)
        {
            if(Items[0] == "stick"){
                item1count = stick;
            }else if(Items[0] == "pizza"){
                item1count = pizza;
            }else if(Items[0] == "key")
            {
                item1count = key;
            } 
            item1.text = Items[0] + " x" + item1count;
        }
        
    }

    void checkItem2()
    {
        if(Items.Length > 1){
            if(Items[1] == "stick")
            {
                item2count = stick;
            }else if(Items[1] == "pizza")
            {
                item2count = pizza;
            }else if(Items[1] == "key"){
                item2count = key;
            } 
            item2.text = Items[1] + " x" + item2count;
        }
    }

    void checkItem3()
    {
        if(Items.Length > 2){
            if(Items[2] == "stick")
            {
                item3count = stick;
            }else if(Items[2] == "pizza")
            {
                item3count = pizza;
            } else if(Items[2] == "key"){
                item3count = key;
            } 
            item3.text = Items[2] + " x" + item3count;
        }
    }

    void checkItem4()
    {
        if(Items.Length > 3){
            if(Items[3] == "stick")
            {
                item4count = stick;
            }else if(Items[3] == "pizza")
            {
                item4count = pizza;
            } else if(Items[3] == "key"){
                item4count = key;
            } 
            item4.text = Items[3] + " x" + item4count;
        }
    }
}
