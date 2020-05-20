using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsyRun : StateMachineBehaviour
{
    private float m_speed = 2f;
    private float m_attackRange = 1.5f;

    private Transform m_playerTransform;
    private Vector2 m_targetPosition;

    private PsyBehaviorComponent m_attack = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_playerTransform = GameObject.FindGameObjectWithTag(ConstValue.TAG_PLAYER).transform;
        m_attack = animator.GetComponent<PsyBehaviorComponent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_targetPosition.x = m_playerTransform.position.x;
        m_targetPosition.y = animator.transform.position.y;

        if (animator.transform.position.x < m_playerTransform.position.x)
        {
            animator.transform.localScale = Vector3.one * 1.5f;
        }
        else if (animator.transform.position.x > m_playerTransform.position.x)
        {
            animator.transform.localScale = ConstValue.FLIP_SCALE * 1.5f;
        }

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_targetPosition, m_speed * Time.deltaTime);

        if (Vector2.Distance(m_playerTransform.position, animator.transform.position) < m_attackRange)
        {
            if (m_playerTransform.GetComponent<HealthComponent>().Hp <= 0)
                return;
            m_attack.Attack();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(ConstValue.TRIGGER_ATTACK);
    }
}
