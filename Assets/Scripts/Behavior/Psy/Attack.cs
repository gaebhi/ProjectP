using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("일반 공격")]
[TaskCategory("ProjectP")]
public class Attack : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Range")]
    public SharedFloat Range = 2;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Target Tag")]
    public SharedString TargetTag;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Move Speed")]
    private SharedFloat Speed = 2;

    private GameObject m_target = null;
    private float sqrMagnitude;
    private Vector2 m_targetPosition;
    
    public override void OnStart()
    {
        sqrMagnitude = Range.Value * Range.Value;

        if (m_target != null)
        {
            m_target = null;
        }

        if (!string.IsNullOrEmpty(TargetTag.Value))
        {
            m_target = GameObject.FindGameObjectWithTag(TargetTag.Value);
            if (transform.GetComponent<PsyBehaviorComponent>() != null)
                transform.GetComponent<PsyBehaviorComponent>().Run();
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (m_target == null)
            return TaskStatus.Failure;

        m_targetPosition.x = m_target.transform.position.x;
        m_targetPosition.y = transform.position.y;

        if (transform.position.x < m_targetPosition.x)
        {
            transform.localScale = Vector3.one * 1.5f;
        }
        else if (transform.position.x > m_targetPosition.x)
        {
            transform.localScale = ConstValue.FLIP_SCALE * 1.5f;
        }

        transform.position = Vector2.MoveTowards(transform.position, m_targetPosition, Speed .Value * Time.deltaTime);

        Vector2 direction;

        direction = m_target.transform.position - transform.position;

        if (Vector2.SqrMagnitude(direction) < sqrMagnitude)
        {
            if (m_target.GetComponent<HealthComponent>().Hp > 0)
                transform.GetComponent<PsyBehaviorComponent>().Attack();
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
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
