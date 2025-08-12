using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BAGetRandomPointOnNavMesh", story: "Origin: [agentGameObject] radius: [radius] result stored in [targetPositon]", category: "CustomAction", id: "38970df4a437c75968791db19ef398b0")]
public partial class BAGetRandomPointOnNavMesh : Action
{
	[SerializeReference]
	public BlackboardVariable<GameObject> agentGameObject;

	[SerializeReference]
	public BlackboardVariable <float> radius;

	[SerializeReference]
	public BlackboardVariable<Vector3> targetPositon;

	protected override Status OnStart()
    {

		targetPositon.Value = GetRandomPointOnNavMesh(agentGameObject.Value.transform.position, radius.Value);

		return Status.Success;
    }

    //protected override Status OnUpdate()
    //{
    //    return Status.Success;
    //}

    //protected override void OnEnd()
    //{
    //}


	Vector3 GetRandomPointOnNavMesh(Vector3 origin, float radius)
	{
		
		Vector3 randomDirection = Random.insideUnitSphere * radius;
		randomDirection += origin;
		randomDirection.y = origin.y; 

		NavMeshHit hit;
		if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
		{
			return hit.position; 
		}

		return Vector3.zero; 
	}

	//bool CanAgentReach(Vector3 targetPoint)
	//{
	//	// Check if the agent can reach the target using CalculatePath
	//	NavMeshPath path = new NavMeshPath();
	//	if (agent.CalculatePath(targetPoint, path))
	//	{
	//		return path.status == NavMeshPathStatus.PathComplete;
	//	}

	//	return false; // If CalculatePath fails, assume unreachable
	//}
}

