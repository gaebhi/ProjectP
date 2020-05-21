using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskDescription("Psy 레이저 공격")]
[TaskCategory("ProjectP")]
public class PsyEyeSkill : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Target Tag")]
    public SharedString TargetTag;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Duration")]
    public SharedFloat Duration = 2f;

    private GameObject m_target = null;
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
            if (transform.GetComponent<PsyBehaviorComponent>() != null)
                transform.GetComponent<PsyBehaviorComponent>().EyeSkill(m_target.transform);
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (m_target == null)
            return TaskStatus.Failure;


        if (m_startTime + Duration.Value < Time.time)
        {
            return TaskStatus.Success;
        }

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
