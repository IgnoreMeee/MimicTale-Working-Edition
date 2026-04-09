using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform spawnpoint;
    public Transform player;
    public Inventorycontroller inventory;
    


   //save and load function works over here, so before battle scene it save, after lose scene it loads!

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Editor mode: Save cleared");
        
    }

    // if (player == null)
    //     {
    //         player = GameObject.FindWithTag("Player").transform;
    //     }

   public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);

        //items
        PlayerPrefs.SetInt("Stick", inventory.stick);
        PlayerPrefs.SetInt("Pizza", inventory.pizza);
        PlayerPrefs.SetInt("Key", inventory.key);
        string itemsString = string.Join(",", inventory.Items);
        PlayerPrefs.SetString("Items", itemsString);

        PlayerPrefs.Save();
    }

    public void PlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX"))
        {

            player.position = new Vector3(3.5f, 34f, 0f);
        }
        else
        {
            player.position = spawnpoint.position;
        }
    }

    public void LoadGame()
    {
        PlayerPosition();

        inventory.stick = PlayerPrefs.GetInt("Stick", 0);
        inventory.pizza = PlayerPrefs.GetInt("Pizza", 0);
        inventory.key = PlayerPrefs.GetInt("Key", 0);


        string itemsString = PlayerPrefs.GetString("Items", "");

        if (!string.IsNullOrEmpty(itemsString))
            {
            inventory.Items = itemsString.Split(',');
        } else {
            inventory.Items = new string[] {};             
        }

        inventory.RefreshUI();

    }
}