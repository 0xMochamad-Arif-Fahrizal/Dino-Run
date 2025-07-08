using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDSprite : MonoBehaviour
{
    public Image[] lifeSprites;  // Array sprite nyawa
    public PlayerHealth playerHealth;  // Referensi ke komponen PlayerHealth

    void Update()
    {
        UpdateLives();
    }

    private void UpdateLives()
    {
        // Dapatkan jumlah nyawa dari PlayerHealth
        int currentLives = playerHealth.GetCurrentLives();

        // Perbarui tampilan sprite
        for (int i = 0; i < lifeSprites.Length; i++)
        {
            if (i < currentLives)
            {
                lifeSprites[i].enabled = true;  // Aktifkan sprite jika nyawa masih ada
            }
            else
            {
                lifeSprites[i].enabled = false; // Nonaktifkan sprite jika nyawa habis
            }
        }
    }
}
