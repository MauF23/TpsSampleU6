using UnityEngine;

public class FootstepRay : MonoBehaviour
{
    public SoundManager soundManager;

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        SurfaceTypeHolder surfaceTypeHolder = other.transform.GetComponent<SurfaceTypeHolder>();

        if (surfaceTypeHolder == null)
        {
            return;
        }

		soundManager.PlaySound(SoundEnums.Steps);
    }
}
