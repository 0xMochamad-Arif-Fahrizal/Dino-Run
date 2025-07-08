using UnityEngine;

public class JumpSound : MonoBehaviour
{
    public AudioSource jumpSound; // Reference ke AudioSource
    private Rigidbody2D rb;       // Untuk mendeteksi lompatan
    public float jumpForce = 5f;  // Kekuatan lompatan

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Deteksi tombol lompat (misalnya tombol Space)
        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            // Lakukan lompatan
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Mainkan efek suara
            if (jumpSound != null)
            {
                jumpSound.Play();
            }
        }
    }
}