using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BAAnimMontageList", story: "CrossFades to the [animator] all [clipList] animations", category: "Action", id: "b585a8173a394f28437dcfb7a9e5f280")]
public partial class BaAnimMontageListAction : Action
{
    [SerializeReference] public BlackboardVariable<Animator> animator;
	[SerializeReference] public BlackboardVariable<AnimClipList> clipList;
    private Coroutine montageCoroutine;
	private bool montageFinished;
	private Awaitable montageAwaitable;

	protected override Status OnStart()
    {
		if(animator.Value == null || clipList.Value == null)
		{
			return Status.Failure;
		}

		montageFinished = false;

		montageAwaitable?.Cancel();
		montageAwaitable = ExecuteMontage();
		return Status.Running;
    }

    protected override Status OnUpdate()
    {
		return montageFinished ? Status.Success : Status.Running;
    }

	protected override void OnEnd()
	{
		montageAwaitable?.Cancel(); 
	}

	private async Awaitable ExecuteMontage()
	{
		foreach (var clip in clipList.Value.animationClips)
		{
			animator.Value.CrossFade(clip.name, 0.1f);
			await Awaitable.WaitForSecondsAsync(clip.length);
		}

		montageFinished = true;
	}
}

