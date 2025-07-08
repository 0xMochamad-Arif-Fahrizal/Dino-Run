using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void BackToMainMenu()
    {
        // Replace "MainMenuScene" with the name of your main menu scene
        SceneManager.LoadScene("MainMenuScene");
    }
}
