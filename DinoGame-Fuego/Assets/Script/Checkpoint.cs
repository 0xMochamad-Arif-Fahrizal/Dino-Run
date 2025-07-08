using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Update checkpoint pada PlayerDeath
            PlayerDeath playerDeath = collision.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                playerDeath.SetCheckpoint(transform.position);
            }

            Debug.Log("Checkpoint Reached!");
        }
    }
}
