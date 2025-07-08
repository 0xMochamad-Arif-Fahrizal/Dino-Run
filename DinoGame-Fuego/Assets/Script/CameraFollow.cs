using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

/*************  ✨ Codeium Command ⭐  *************/
/// <summary>
/// Updates the camera position to smoothly follow the target with a specified offset.
/// This method interpolates between the current camera position and the desired position
/// using a configurable smooth speed for smooth movement. Optionally, the camera can
/// be set to always face the target.
/// </summary>
/******  66232dbf-0ceb-4615-bdac-e0e670820583  *******/    [SerializeField] private Transform target;

    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
} 