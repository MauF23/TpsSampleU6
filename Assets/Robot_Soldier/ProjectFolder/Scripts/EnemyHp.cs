using Unity.Behavior;
using UnityEngine;

public class EnemyHp : Hp
{
	public BehaviorGraphAgent graphAgent;
	public Animator animator;

	private const string HIT_TRIGGER = "Hit";
	private const string DEATH_TRIGGER = "Death";
	private const string ENEMY_STATE_VARIABLE = "EnemyState";

	public override void ReduceHp(int amount)
	{
		if (!Alive())
		{
			return;
		}

		base.ReduceHp(amount);

		if (!Alive())
		{
			return;
		}

		animator?.SetTrigger(HIT_TRIGGER);

		ForceToAggro();
	}

	protected override void Death()
	{
		animator?.SetTrigger(DEATH_TRIGGER);
		graphAgent.Graph.End();
		
		graphAgent.enabled = false;
	}

	private bool OnAggroState()
	{
		BlackboardVariable enemyState;

		if (graphAgent.GetVariable(ENEMY_STATE_VARIABLE, out enemyState))
		{
			EnemyState value = (EnemyState)enemyState.ObjectValue;

			if(value == EnemyState.Aggro || value == EnemyState.Attack || value == EnemyState.Death)
			{
				return true;
			}
		}

		return false;
	}

	public void ForceToAggro()
	{
		if (!OnAggroState())
		{
			graphAgent.SetVariableValue(ENEMY_STATE_VARIABLE, EnemyState.Aggro);
		}
	}
}
