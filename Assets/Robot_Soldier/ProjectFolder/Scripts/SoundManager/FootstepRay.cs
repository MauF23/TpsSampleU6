using UnityEngine;

public class FootstepRay : MonoBehaviour
{
	public SoundManager soundManager;
	private int stepCounter = 0;

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

		stepCounter++;

		if(stepCounter <= 1)
		{
			return;
		}

		switch (surfaceTypeHolder.surfaceType)
		{
			case SurfacesEnums.Concrete:
				soundManager.PlaySoundRandomPitch(SoundEnums.Steps, 0.85f, 1.20f);
				break;

			case SurfacesEnums.Rug:
				soundManager.PlaySoundRandomPitch(SoundEnums.StepRug, 0.85f, 1.20f);
				break;

			default:
				soundManager.PlaySoundRandomPitch(SoundEnums.Steps, 0.85f, 1.20f);
				break;
		}
	}
}
