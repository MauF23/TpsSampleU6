using UnityEngine;
using DG.Tweening;
using System;

public class AmmoBox : Interactable
{
    public Transform boxLid;
    public Vector3 tweenDegrees;
    public float tweenTime, delayCloseTime;
    private Sequence sequenceOpenBox;

    protected override void Interact()
    {
        base.Interact();
        sequenceOpenBox?.Kill();

        Action reloadAction = () => playerInRange.currentWeapon?.AddReserveAmmo(playerInRange.currentWeapon.maxAmmoCapacity);

        sequenceOpenBox = DOTween.Sequence();
        sequenceOpenBox.Append(boxLid.DOLocalRotate(tweenDegrees, tweenTime).OnStart(() => reloadAction?.Invoke()).SetEase(Ease.OutBounce)); //OpenBox
        sequenceOpenBox.Append(boxLid.DOLocalRotate(Vector3.zero, tweenTime).SetDelay(delayCloseTime)); //CloseBox

        sequenceOpenBox.OnComplete(() => canInteract = true);

    }

}
