using UnityEngine;
using UnityEngine.Tilemaps;

public class RefreshCollider : MonoBehaviour
{
    private TilemapCollider2D tilemapCollider;

    void Start()
    {
        // Ensure you have a TilemapCollider2D component attached to this GameObject
        tilemapCollider = GetComponent<TilemapCollider2D>();

        if (tilemapCollider != null)
        {
            // Refresh or reset the collider here if needed
            tilemapCollider.enabled = false;  // Disable collider temporarily
            tilemapCollider.enabled = true;   // Re-enable collider
        }
        else
        {
            Debug.LogError("TilemapCollider2D component is missing.");
        }
    }
}
