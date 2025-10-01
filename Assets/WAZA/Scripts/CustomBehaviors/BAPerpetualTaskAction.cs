using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BAPerpetualTask", story: "Perpetual Task, will always return running", category: "Action", id: "334188cdb56ca3e2f66c7b7bbc7bf9de")]
public partial class BAPerpetualTaskAction : Action
{

    protected override Status OnStart()
    {
        return Status.Running;
    }

    //protected override Status OnUpdate()
    //{
    //    return Status.Running;
    //}

    //protected override void OnEnd()
    //{
    //}
}

