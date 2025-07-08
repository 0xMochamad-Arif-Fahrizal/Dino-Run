using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class PortalEnding : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private string targetSceneName = "FinishScene"; // Scene name to load
    [SerializeField] private Collider2D portalCollider; // Reference to the portal collider

    private void Awake()
    {
        if (portalCollider == null)
        {
            portalCollider = GetComponent<Collider2D>();
            if (portalCollider == null)
            {
                Debug.LogError("PortalEnding requires a Collider2D to function.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Object entered portal: {collision.gameObject.name}"); // Debugging log

        if (!collision.CompareTag("Player")) // Ensure only the player can trigger the portal
        {
            Debug.Log("Object is not the player, ignoring.");
            return;
        }

        if (portalObjects.Contains(collision.gameObject))
        {
            Debug.Log("Object already in portal, ignoring.");
            return;
        }

        portalObjects.Add(collision.gameObject);

        Debug.Log("Player entered portal, loading scene...");
        // Transition to the specified scene when the player enters the portal
        SceneManager.LoadScene(targetSceneName);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"Object exited portal: {collision.gameObject.name}"); // Debugging log
        portalObjects.Remove(collision.gameObject);
    }

    private void OnDrawGizmos()
    {
        // Visualize the portal area in the editor
        Gizmos.color = Color.cyan;
        if (portalCollider != null)
        {
            Gizmos.DrawWireCube(portalCollider.bounds.center, portalCollider.bounds.size);
        }
    }
}
