using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BALookAtTarget", story: "[Agent] looks at [Target]", category: "Action", id: "e1b38c28e66177228e4f37a098106593")]
public partial class BALookAtTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> RotationSpeed;

	protected override Status OnStart()
    {
        if(Agent.Value == null || Agent.Value == null)
        {
            return Status.Failure;
        }


        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        LookAtTarget();
		return Status.Success;
    }

    //protected override void OnEnd()
    //{
    //}

    private void LookAtTarget()
    {
        Vector3 direction = Target.Value.transform.position - Agent.Value.transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        Agent.Value.transform.rotation = Quaternion.Lerp(Agent.Value.transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

	}
}

