using UnityEngine;

public class LightFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Objek player yang akan diikuti
    [SerializeField] private Vector3 offset = Vector3.zero; // Offset cahaya terhadap player

    private void LateUpdate()
    {
        if (player != null)
        {
            // Posisi light di-update untuk mengikuti posisi player ditambah offset
            transform.position = player.position + offset;
        }
        else
        {
            Debug.LogWarning("Player Transform belum diassign di LightFollow script!", this);
        }
    }
}
