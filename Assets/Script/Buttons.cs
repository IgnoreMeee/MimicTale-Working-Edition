using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    public void QuitButton()
    {
        Debug.Log("quit!");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
    }
    public void LeaveButton()
    {
        Debug.Log("quit!");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
    }

}
