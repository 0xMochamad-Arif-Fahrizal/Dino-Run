using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        // Ambil komponen Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Mengambil input horizontal (A, D, atau arrow keys)
        horizontal = Input.GetAxisRaw("Horizontal");

        // Menggunakan Raycast untuk mendeteksi apakah karakter berada di tanah
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        isGrounded = hit.collider != null;

        // Melakukan aksi lompat jika di tanah
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            animator.SetTrigger("Jump"); // Memicu animasi lompat
        }

        // Mengurangi kecepatan lompat jika tombol jump dilepas lebih awal
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // Update animasi berjalan
        animator.SetBool("isWalking", horizontal != 0);
        animator.SetBool("isGrounded", isGrounded);

        // Membalik karakter jika bergerak ke arah berlawanan
        Flip();
    }

    private void FixedUpdate()
    {
        // Mengatur kecepatan horizontal karakter
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
