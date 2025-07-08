using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    private bool isCollected = false;

    void Start()
    {
        // Mulai animasi melambung
        StartBounceAnimation();
    }

    void StartBounceAnimation()
    {
        // Simpan posisi awal
        Vector3 startPosition = transform.position;

        // Buat animasi naik dan turun
        LeanTween.moveY(gameObject, startPosition.y + 0.5f, 0.5f)
            .setEaseOutCubic()
            .setLoopPingPong(); // Ulangi naik-turun
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected && collision.CompareTag("Player"))
        {
            isCollected = true; // Mencegah pengumpulan berulang

            // Tambahkan koin ke CoinManager
            CoinManager.instance.AddCoin(0);

            // Mainkan suara jika ada
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            // Hentikan animasi LeanTween
            LeanTween.cancel(gameObject);

            // Nonaktifkan tampilan koin agar terlihat "terkumpul"
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Hancurkan objek setelah suara selesai dimainkan
            float destroyDelay = (audioSource != null && audioSource.clip != null)
                ? audioSource.clip.length
                : 0.1f;
            Destroy(gameObject, destroyDelay);
        }
    }

}

