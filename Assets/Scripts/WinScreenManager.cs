using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    // Method to be called when the Main Menu button is clicked
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); // Ensure your title screen scene is named "TitleScreen"
    }

    // Method to be called when the Quit Game button is clicked
    public void QuitGame()
    {
        Application.Quit();
    }
}