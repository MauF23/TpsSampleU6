using UnityEngine;

public class PlayerHp : Hp
{
	public PostProcessingManager postProcessingManager;
	public override void ReduceHp(int amount)
	{
		postProcessingManager?.VignetteFx(Color.red, 0.5f, 0.2f, 4);
		base.ReduceHp(amount);
	}
}
