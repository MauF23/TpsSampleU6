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
                soundManager.PlayRandomPitch("Step", 0.75f, 1.25f);
                break;

            case "Rug":
                soundManager.PlayRandomPitch("StepSoft", 0.75f, 1.25f);
                break;
        }
    }
}
