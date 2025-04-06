using UnityEngine;
using DG.Tweening;
using System.Collections;

public class DroneHp : Hp
{
    [SerializeField]
    private float reviveTime;
    private float scaleTweenTime = 0.5f;

	protected override void Death()
	{
		base.Death();
        StartCoroutine(ReviveRoutine(reviveTime));
	}

    private IEnumerator ReviveRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        visualReferenceGameObject.transform.localScale = Vector3.zero;
        Revive();
		visualReferenceGameObject.transform.DOScale(Vector3.one, scaleTweenTime).SetEase(Ease.OutBounce);

	}
}
