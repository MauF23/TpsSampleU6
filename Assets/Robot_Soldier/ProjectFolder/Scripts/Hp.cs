using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Hp : MonoBehaviour
{

	public int startingHp;

	public UnityEvent onRemoveHpEvent, onDeathEvent, onReviveEvent;

	public GameObject visualReferenceGameObject;

	protected int currentHp;

	protected virtual void Start()
	{
		currentHp = startingHp;
	}

	public virtual void ReduceHp(int amount)
	{
		currentHp -= amount;
		currentHp = ClampHp(currentHp);

		if (currentHp <= 0)
		{
			Death();
			return;
		}

		onRemoveHpEvent?.Invoke();
	}

	public virtual void AumentHp(int amount)
	{
		currentHp += amount;
		currentHp = ClampHp(currentHp);
	}

	protected virtual int ClampHp(int hpValue)
	{
		return Mathf.Clamp(currentHp, 0, startingHp);
	}

	protected virtual void Death()
	{
		onDeathEvent?.Invoke();
		visualReferenceGameObject.SetActive(false);
	}

	protected virtual void Revive()
	{
		currentHp = startingHp;
		visualReferenceGameObject.SetActive(true);
		onReviveEvent?.Invoke();

	}
}
