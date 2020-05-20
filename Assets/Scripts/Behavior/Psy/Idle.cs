using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskDescription("Idle Wait")]
[TaskCategory("ProjectP")]
public class Idle : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Idle")]
    public SharedFloat WaitTime = 1;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("Is Random?")]
    public SharedBool RandomWait = false;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("random min")]
    public SharedFloat RandomWaitMin = 1;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("random max")]
    public SharedFloat RandomWaitMax = 1;

    private float m_waitDuration;
    private float m_startTime;
    private float m_pauseTime;

    public override void OnStart()
    {
        if(transform.GetComponent<PsyBehaviorComponent>() != null)
            transform.GetComponent<PsyBehaviorComponent>().Idle();

        m_startTime = Time.time;
        if (RandomWait.Value)
        {
            m_waitDuration = Random.Range(RandomWaitMin.Value, RandomWaitMax.Value);
        }
        else
        {
            m_waitDuration = WaitTime.Value;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (m_startTime + m_waitDuration < Time.time)
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
        WaitTime = 1;
        RandomWait = false;
        RandomWaitMin = 1;
        RandomWaitMax = 1;
    }
}
