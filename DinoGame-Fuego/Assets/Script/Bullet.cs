using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5f;  // Kecepatan peluru
    private float maxDistance = 10f; // Jarak maksimum sebelum peluru dihancurkan
    public LayerMask playerLayer;   // Layer pemain untuk deteksi tabrakan

    private Vector3 startPosition;  // Posisi awal peluru
    private Animator animator;      // Animator peluru

    void Start()
    {
        // Set posisi awal peluru
        startPosition = transform.position;

        // Inisialisasi Animator
        animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        // Mulai animasi peluru
        if (animator != null)
        {
            animator.SetTrigger("StartAnimation");
        }
    }

    public void SetMaxDistance(float distance)
    {
        // Set nilai maxDistance
        maxDistance = distance;
    }

    void Update()
    {
        // Gerakkan peluru sesuai dengan kecepatan dan arah
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);

        // Hitung jarak yang sudah ditempuh peluru
        float traveledDistance = Vector3.Distance(startPosition, transform.position);

        // Hancurkan peluru jika sudah melewati jarak maksimal
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika peluru menabrak objek dengan layer pemain
        if (IsPlayer(collision.gameObject))
        {
            Destroy(gameObject);  // Hancurkan peluru
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Jika peluru menabrak objek dengan layer pemain
        if (IsPlayer(collider.gameObject))
        {
            Destroy(gameObject);  // Hancurkan peluru
        }
    }

    private bool IsPlayer(GameObject obj)
    {
        return ((1 << obj.layer) & playerLayer) != 0;
    }
}
