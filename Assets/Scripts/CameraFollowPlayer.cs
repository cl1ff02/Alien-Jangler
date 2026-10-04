using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        // Automatically calculate the current starting offset
        if (target != null)
        {
            offset = transform.position - target.position; //
        }
    }

    void LateUpdate() // Use LateUpdate to avoid stuttering/jittering
    {
        if (target != null)
        {
            // Maintain the fixed world-space offset
            transform.position = target.position + offset; //
        }
    }
}
