using UnityEngine;
using UnityEngine.SceneManagement;
public class battletrigger : MonoBehaviour
{
    public CheckPoint checkpoint;
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();

            if (player != null && player.isInvincible) return;

            checkpoint.SaveGame();
            SceneManager.LoadScene("LoseScene");
        }
    }
}