using UnityEngine;

public class FootstepRay : MonoBehaviour
{
    public SoundManager soundManager;

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
			case "Concrete":
				soundManager.PlaySound(SoundEnums.Steps);
				break;

			case "Rug":
				soundManager.PlaySound(SoundEnums.StepRug);
				break;
		}
    }
}
