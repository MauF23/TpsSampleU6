using StarterAssets;
using UnityEngine;

public class AnimEventsWrapper : MonoBehaviour
{
    public ThirdPersonController thirdPersonController;

    public void ReloadEvent()
    {
        if (thirdPersonController == null)
        {
            return;
        }

        thirdPersonController.currentWeapon?.Reload();
        Debug.Log($"ReloadEvent Triggered");
    }
}
