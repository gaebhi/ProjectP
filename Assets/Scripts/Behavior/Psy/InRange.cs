using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using BehaviorDesigner.Runtime;

[TaskDescription("범위안에 있는지 검사")]
[TaskCategory("ProjectP")]
[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}WithinDistanceIcon.png")]
public class InRange : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Range")]
    public SharedFloat Range = 5;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Target Tag")]
    public SharedString TargetTag;

    private GameObject m_player = null;
    private float sqrMagnitude;

    public override void OnStart()
    {
        sqrMagnitude = Range.Value * Range.Value;

        if (m_player != null)
        {
            m_player = null;
        }

        if (!string.IsNullOrEmpty(TargetTag.Value))
        {
            m_player = GameObject.FindGameObjectWithTag(TargetTag.Value);
        }
    }

    // returns success if any object is within distance of the current object. Otherwise it will return failure
    public override TaskStatus OnUpdate()
    {
        if (m_player == null)
            return TaskStatus.Failure;

        Vector3 direction;

        direction = m_player.transform.position - transform.position;

        if (Vector3.SqrMagnitude(direction) < sqrMagnitude)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

    public override void OnReset()
    {
        TargetTag = string.Empty;
        Range = 5;
    }

    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Range == null)
        {
            return;
        }
        var oldColor = UnityEditor.Handles.color;
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(Owner.transform.position, Owner.transform.forward, Range.Value);
        UnityEditor.Handles.color = oldColor;
#endif
    }
}
