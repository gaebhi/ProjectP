using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsyIdle : StateMachineBehaviour
{
    private float m_resetTime;
    private float m_minTime = 1f;
    private float m_maxTime = 3f;

    private Transform m_playerTransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_resetTime = UnityEngine.Random.Range(m_minTime, m_maxTime);

        m_playerTransform = GameObject.FindGameObjectWithTag(ConstValue.TAG_PLAYER).transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_resetTime <= 0)
        {
            animator.SetTrigger(ConstValue.TRIGGER_SKILL3);
        }
        else
        {
            m_resetTime -= Time.deltaTime;
        }
    }
}
