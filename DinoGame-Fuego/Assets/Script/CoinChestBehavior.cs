using UnityEngine;

public class CoinChestBehavior : MonoBehaviour
{
    private AudioSource audioSource;
    public int coinAmount = 1; // Jumlah Coin yang ditambahkan
    private bool isCollected = false; // Untuk memastikan Coin hanya terkoleksi sekali

    void Start()
    {
        // Ambil AudioSource dari GameObject ini
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected && collision.CompareTag("Player"))
        {
            isCollected = true; // Tandai Coin telah terkoleksi

            // Mainkan suara jika ada
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            // Tambahkan koin ke CoinManager sesuai jumlah Coin
            CoinManager.instance.AddCoin(coinAmount);

            // Nonaktifkan tampilan dan collider
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Hancurkan setelah suara selesai dimainkan
            float destroyDelay = (audioSource != null && audioSource.clip != null)
                ? audioSource.clip.length
                : 0.1f;
            Destroy(gameObject, destroyDelay);
        }
    }
}
