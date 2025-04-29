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

        switch (surfaceTypeHolder.surfaceType)
        {
			case SurfacesEnums.Concrete:
				soundManager.PlayAudioRandomPitch(SoundEnums.Steps, 0.85f, 1.25f);
				break;

			case SurfacesEnums.Rug:
				soundManager.PlayAudioRandomPitch(SoundEnums.StepRug, 0.85f, 1.25f);
				break;

            default:
				soundManager.PlayAudioRandomPitch(SoundEnums.Steps, 0.85f, 1.25f);
				break;
		}
    }
}
