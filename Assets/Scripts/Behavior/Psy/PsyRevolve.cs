using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("회전 공격")]
[TaskCategory("ProjectP")]
public class PsyRevolve : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Target Tag")]
    public SharedString TargetTag;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Move Speed")]
    private SharedFloat Speed = 3.5f;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Duration")]
    public SharedFloat Duration = 2f;

    private GameObject m_target = null;
    private Vector2 m_targetPosition;
    private PsyBehaviorComponent m_psy = null;
    private float m_startTime;
    private float m_pauseTime;

    public override void OnStart()
    {
        if (m_target != null)
        {
            m_target = null;
        }

        m_startTime = Time.time;

        if (!string.IsNullOrEmpty(TargetTag.Value))
        {
            m_target = GameObject.FindGameObjectWithTag(TargetTag.Value);

            if (m_target != null)
            {
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

                m_psy = GetComponent<PsyBehaviorComponent>();

                if (m_psy != null)
                    m_psy.Revolve();
            }

        }
    }

    public override TaskStatus OnUpdate()
    {
        if (m_target == null || m_psy == null)
            return TaskStatus.Failure;

        if (m_startTime + Duration.Value < Time.time)
        {
            return TaskStatus.Success;
        }

        transform.position = Vector2.MoveTowards(transform.position, m_targetPosition, Speed.Value * Time.deltaTime);
        m_psy.RevolveAttack();

        return TaskStatus.Running;
    }

    public override void OnPause(bool paused)
    {
        if (paused)
        {
            m_pauseTime = Time.time;
        }
        else
        {
            m_startTime += (Time.time - m_pauseTime);
        }
    }

    public override void OnReset()
    {
        TargetTag = string.Empty;
    }
}
