using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Ambil AudioSource dari GameObject ini
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Mainkan suara jika ada
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Tambahkan koin ke GameManager
            CoinManager.instance.AddCoin(1);

            // Nonaktifkan objek agar tidak terlihat, tetapi tunda penghancuran
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Hancurkan setelah suara selesai dimainkan
            if (audioSource != null)
                Destroy(gameObject, audioSource.clip.length);
            else
                Destroy(gameObject, 0.1f); // Default jika tidak ada suara
        }
    }
}
