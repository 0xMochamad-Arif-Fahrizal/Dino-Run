using UnityEngine;

public class InvisibleBlockDetector : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Melakukan Raycast ke bawah untuk mendeteksi apakah ada collider invisible block
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        
        // Jika raycast mendeteksi sesuatu (collider)
        if (hit.collider != null)
        {
            // Menampilkan nama collider yang terdeteksi pada log
            Debug.Log("Invisible Block Detected: " + hit.collider.name);
        }
    }
}
