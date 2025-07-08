using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 initialSpawnPosition; // Posisi spawn awal
    public Vector3 respawnPosition { get; private set; } // Ubah menjadi public dengan getter (dan setter opsional)

    public LayerMask hazardLayer;         // Layer untuk hazard
    public AudioSource deathSFX;          // Efek suara kematian
    public PlayerHealth playerHealth;     // Referensi ke skrip PlayerHealth
    public int damageAmount = 1;          // Jumlah damage
    public GameOverManager gameOverManager; // Referensi ke GameOverManager

    void Start()
    {
        // Set posisi spawn awal dan respawn
        initialSpawnPosition = transform.position;
        respawnPosition = initialSpawnPosition;

        // Jika deathSFX belum diatur, coba mencari AudioSource pada game object ini
        if (deathSFX == null)
        {
            deathSFX = GetComponent<AudioSource>();
        }

        // Cari referensi ke PlayerHealth jika belum diatur
        if (playerHealth == null)
        {
            playerHealth = GetComponent<PlayerHealth>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & hazardLayer) != 0 || collision.gameObject.CompareTag("Bullet"))
        {
            HandlePlayerDamage();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & hazardLayer) != 0 || other.CompareTag("Bullet"))
        {
            HandlePlayerDamage();
        }
    }

    void HandlePlayerDamage()
    {
        playerHealth.TakeDamage(damageAmount);

        if (playerHealth.GetCurrentLives() <= 0)
        {
            if (respawnPosition != initialSpawnPosition) // Jika ada checkpoint
            {
                RespawnAtCheckpoint();
            }
            else // Jika belum ada checkpoint
            {
                TriggerGameOver();
            }
        }
    }

    void RespawnAtCheckpoint()
    {
        transform.position = respawnPosition;

        // Mainkan efek suara kematian jika ada
        if (deathSFX != null)
        {
            deathSFX.Play();
        }

        Debug.Log("Player respawned at checkpoint!");

        // Reset nyawa pemain setelah respawn
        playerHealth.ResetLives(playerHealth.maxLives);
    }

    void TriggerGameOver()
    {
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogError("GameOverManager is not assigned!");
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        respawnPosition = checkpointPosition;
        Debug.Log("Checkpoint updated to: " + checkpointPosition);
    }
}