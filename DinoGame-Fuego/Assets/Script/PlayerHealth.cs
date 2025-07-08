using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;   // Jumlah nyawa maksimum
    private int currentLives; // Nyawa saat ini
    private bool isGameOver = false; // Menandakan apakah pemain sudah mati

    void Start()
    {
        // Set nyawa awal
        ResetLives(maxLives);
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver) return; // Jangan lanjutkan jika game sudah berakhir

        // Kurangi nyawa
        currentLives -= damage;

        // Cek apakah nyawa habis
        if (currentLives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isGameOver = true; // Set status game over
        Debug.Log("Player has no lives left!");

        // Panggil GameOverManager untuk memulai transisi
        GameOverManager gameOverManager = Object.FindFirstObjectByType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogError("GameOverManager tidak ditemukan di scene!");
        }
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public void ResetLives(int maxLives)
    {
        this.maxLives = maxLives; // Atur ulang nyawa maksimum
        currentLives = maxLives; // Reset nyawa saat ini
        isGameOver = false;      // Reset status game over
        Debug.Log("Lives reset to: " + currentLives);
    }
}