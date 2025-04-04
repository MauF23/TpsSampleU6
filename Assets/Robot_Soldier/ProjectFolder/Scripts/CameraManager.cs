using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera playerCamera;
    public Transform aimHelper, aimVisualHelper;
    public LayerMask aimLayer;
    private const float DEFAULT_AIM_RANGE = 100;
    public static CameraManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        aimHelper.transform.position = Aim();
    }

    public Vector3 Aim()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, DEFAULT_AIM_RANGE, aimLayer))
        {
            //Debug.Log($"RayHitted: {hit.transform.name}");
            return hit.point;
        }
        else
        {
            return ray.direction * DEFAULT_AIM_RANGE;
        }
    }
}
