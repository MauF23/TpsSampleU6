using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set NavAgentSpeed", story: "[Agent] walk speed set to [speed]", category: "Action", id: "13594bf26d4a24efdf56be0864518965")]
public partial class BASetNavAgentSpeedAction : Action
{
	[SerializeReference] public BlackboardVariable<GameObject> Agent;
	[SerializeReference] public BlackboardVariable<float> Speed;
    private NavMeshAgent navMeshAgent;

    protected override Status OnStart()
    {
        if(navMeshAgent == null)
        {
			navMeshAgent = Agent.Value.GetComponent<NavMeshAgent>();
		}

        return SetNavMeshAgentSpeed(navMeshAgent);
    }

    Status SetNavMeshAgentSpeed(NavMeshAgent navMeshAgent)
    {
        if(navMeshAgent == null)
        {
            return Status.Failure;
        }

        navMeshAgent.speed = Speed.Value;
		return Status.Success;

	}

    //protected override Status OnUpdate()
    //{
    //    return Status.Success;
    //}

    //protected override void OnEnd()
    //{
    //}
}

