using UnityEngine;
using UnityEngine.Animations.Rigging;
using DG.Tweening;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
	private const string SPEED = "Speed";

	void Start()
    {

	}
	
	public void Grounded(bool value)
	{

	}
	public void Jump(bool value)
	{

	}

	public void Aim(bool value)
	{

	}

	public void Falling(bool value)
	{

	}

	public void Hit()
	{

	}

	public void Reaload()
	{

	}

	public void SetMovement(float movementSpeed)
	{
		animator?.SetFloat(SPEED, movementSpeed);
	}
}
