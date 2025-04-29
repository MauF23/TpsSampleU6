using UnityEngine;

public class FootstepRay : MonoBehaviour
{
    public float rayLenght;
    public SoundManager soundManager;

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        SurfaceTypeHolder surfaceTypeHolder = other.transform.GetComponent<SurfaceTypeHolder>();

        if (surfaceTypeHolder != null) 
        {
            Debug.Log($"<color=lime>hittedSurface: {other.transform.name} with Type {surfaceTypeHolder.surfaceType}</color>");
        }
	}
}
