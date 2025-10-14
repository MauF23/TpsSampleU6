using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;

	#region Constants
	private const string SPEED = "Speed";
	private const string AIM = "Aiming";
	private const string GROUND = "Grounded";
	private const string FALL = "FreeFall";
	private const string JUMP = "Jump";
	private const string MOTION = "MotionSpeed";
	private const string MOVEX = "MoveDirX";
	private const string MOVEZ = "MoveDirZ";
	private const float RIG_TWEEN_TIME = 0.15f;
	#endregion

	void Start()
    {
	}
	
	public void Grounded(bool value)
	{
		animator?.SetBool(GROUND, value);
	}
	public void Jump(bool value)
	{
		animator?.SetBool(JUMP, value);
	}

	public void Aim(bool value)
	{
		animator?.SetBool(AIM, value);
	}

	public void Falling(bool value)
	{
		animator?.SetBool(FALL, value);
	}

	public void SetMovement(Vector3 movementVector, float movementSpeed)
	{
		animator?.SetFloat(SPEED, movementSpeed);

		animator?.SetFloat(MOVEX, movementVector.normalized.x, 0.1f, Time.deltaTime);
		animator?.SetFloat(MOVEZ, movementVector.normalized.z, 0.1f, Time.deltaTime);
	}
}
