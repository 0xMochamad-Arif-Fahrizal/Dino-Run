using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Kecepatan gerakan musuh
    public Transform leftPoint; // Titik kiri batas gerakan
    public Transform rightPoint; // Titik kanan batas gerakan

    private bool movingRight = true; // Arah gerakan saat ini
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

            // Jika musuh melewati batas kanan, ubah arah
            if (transform.position.x >= rightPoint.position.x)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);

            // Jika musuh melewati batas kiri, ubah arah
            if (transform.position.x <= leftPoint.position.x)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        // Membalikkan arah sprite musuh
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
