using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera playerCamera;
    public Transform aimHelper;
    public LayerMask aimLayer;
    private const float DEFAULT_AIM_RANGE = 100;
    
    void Update()
    {
        aimHelper.transform.position = Aim();
    }

    Vector3 Aim()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, aimLayer))
        {
            return hit.point;
        }

        return ray.direction * DEFAULT_AIM_RANGE;
    }
}
