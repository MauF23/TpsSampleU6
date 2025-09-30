using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BAStopNavAgent", story: "Sets [navMeshAgent] Stop to [Bool]", category: "Action", id: "0b30f76849c10f780a3d7890948d6db1")]
public partial class BAStopNavAgentAction : Action
{
	[SerializeReference] public BlackboardVariable<NavMeshAgent> navMeshAgent;
	[SerializeReference] public BlackboardVariable<bool> Bool;

    protected override Status OnStart()
    {
        if(navMeshAgent.Value == null)
        {
            return Status.Failure;
        }

		navMeshAgent.Value.isStopped = Bool.Value;
		return Status.Running;
    }
}

