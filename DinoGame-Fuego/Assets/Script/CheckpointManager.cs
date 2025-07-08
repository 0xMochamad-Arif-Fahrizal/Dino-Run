using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector3 currentCheckpoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        currentCheckpoint = checkpointPosition;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (currentCheckpoint != Vector3.zero)
        {
            player.transform.position = currentCheckpoint;
        }
        else
        {
            Debug.LogWarning("Checkpoint not set!");
        }
    }
}
