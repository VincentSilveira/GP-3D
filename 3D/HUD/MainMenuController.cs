using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Clicked On Play");
        SceneManager.LoadScene("HUD");
    }

    public void QuitGame()
    {
        Debug.Log("Clicked on Quit");
        Application.Quit();
    }
}
