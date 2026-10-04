using UnityEngine;

public class WorldCrosshairController : MonoBehaviour
{
    [SerializeField] private RectTransform crosshairUI;
    [SerializeField] private Camera aimCamera;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float crosshairOffsetMultiplier = 0.01f;
    [SerializeField] private LayerMask raycastMask = ~0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Ray ray = aimCamera.ScreenPointToRay(screenCenter);
        Vector3 targetPosition;
        if(Physics.Raycast(ray, out RaycastHit hit, maxDistance, raycastMask))
        {
            targetPosition = hit.point + hit.normal * crosshairOffsetMultiplier;
            crosshairUI.rotation = Quaternion.LookRotation(hit.normal);
            Debug.DrawLine(hit.point, hit.point + hit.normal * 2f, Color.green);
        }
        else
        {
            targetPosition = ray.GetPoint(maxDistance);
            crosshairUI.forward = aimCamera.transform.forward;
        }
        crosshairUI.position = targetPosition;
    }
}
