using UnityEngine;

public class PlayerHp : Hp
{
	public PostProcessManager postProcessManager;
	public override void ReduceHp(int amount)
	{
		postProcessManager?.VignetteFx(Color.red, 0.5f, 0.2f, 4);

        base.ReduceHp(amount);
	}
}
