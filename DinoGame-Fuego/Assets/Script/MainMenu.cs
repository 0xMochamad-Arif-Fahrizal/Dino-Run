using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionsPanel; // Drag your Options Panel here in Inspector

    private void Start()
    {
        // Pastikan panel options tidak terlihat di awal
        optionsPanel.SetActive(false);
    }

    // Fungsi untuk memulai permainan
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Ganti "GameScene" dengan nama scene game Anda
    }

    // Fungsi untuk membuka panel options
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    // Fungsi untuk menutup panel options
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    // Fungsi untuk keluar dari game
    public void QuitGame()
    {
        Debug.Log("Game Quit!");
        Application.Quit(); // Berfungsi hanya dalam build game
    }
}
