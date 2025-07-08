using UnityEngine;

public class CannonLeft : MonoBehaviour
{
    public GameObject bulletPrefab;      // Prefab peluru yang akan ditembakkan
    public Transform firePoint;         // Titik tempat peluru akan ditembakkan
    public float fireRate = 12f;        // Kecepatan tembakan (tembakan per detik)
    public float detectionRange = 7f;   // Jarak deteksi pemain
    public float bulletMaxDistance = 10f; // Jarak maksimal default peluru
    public LayerMask playerLayer;       // Layer pemain

    private float lastFiredTime = 0f;   // Waktu tembakan terakhir

    void Update()
    {
        // Deteksi pemain dalam jarak tertentu
        if (PlayerInLeftRange())
        {
            // Cek apakah sudah waktunya menembak
            if (Time.time - lastFiredTime >= 1f / fireRate)
            {
                FireBullet();
                lastFiredTime = Time.time; // Set waktu tembakan terakhir
            }
        }
    }

    // Mengecek apakah pemain berada dalam jarak deteksi di sebelah kiri dan sejajar horizontal
    bool PlayerInLeftRange()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        if (player != null)
        {
            // Pastikan pemain berada di sebelah kiri cannon dan sejajar secara vertikal
            float verticalThreshold = 1f; // Toleransi perbedaan tinggi antara cannon dan pemain
            return player.transform.position.x < transform.position.x &&
                   Mathf.Abs(player.transform.position.y - transform.position.y) <= verticalThreshold;
        }
        return false;
    }

    // Mencegah pemain menembus objek cannon
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; // Hentikan gerakan pemain
            }
        }
    }

    // Menembakkan peluru
    void FireBullet()
    {
        // Instantiate peluru
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Atur Rigidbody2D untuk gerakan peluru
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Arah tembakan selalu ke kiri
            Vector2 direction = Vector2.left;

            // Memberikan kecepatan pada peluru sesuai arah
            rb.linearVelocity = direction * 5f;

            // Pastikan rotasi peluru mengikuti arah tembakan
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        // Atur maxDistance dan mulai animasi peluru
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetMaxDistance(bulletMaxDistance); // Set jarak tembak peluru
            bulletScript.StartAnimation(); // Mulai animasi
        }
    }
}
