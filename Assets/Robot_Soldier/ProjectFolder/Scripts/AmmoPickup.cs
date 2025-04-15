using UnityEngine;

public class AmmoPickup : Interactable
{
    public int ammoToAdd;
    protected override void Interact()
    {
        base.Interact();
        playerInRange.currentWeapon?.AddReserveAmmo(ammoToAdd);
        gameObject.SetActive(false);

    }
}
