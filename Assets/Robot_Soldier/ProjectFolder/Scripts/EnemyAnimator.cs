using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
	public NavMeshAgent agent;
	public Animator animator;
	public const string WALK_SPEED = "WalkSpeed";

	private void Update()
	{
		SetWalkSpeed();
	}

	public void SetWalkSpeed()
	{
		animator?.SetFloat(WALK_SPEED, Mathf.Abs(agent.velocity.magnitude));
	}
}
