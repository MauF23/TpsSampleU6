using StarterAssets;
using UnityEngine;

public class AnimEventsWrapper : MonoBehaviour
{
    public ThirdPersonController thirdPersonController;
    public SoundManager soundManager;

    public void ReloadEvent()
    {
        if (thirdPersonController == null)
        {
            return;
        }

        thirdPersonController.currentWeapon?.Reload();
        soundManager.PlaySound("Reload");

		Debug.Log($"ReloadEvent Triggered");
    }
}
