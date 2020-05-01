using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsySkill2 : StateMachineBehaviour
{
    private int m_randomValue;
    private float m_resetTime = 0.7f;
    private Transform m_playerTransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_playerTransform = GameObject.FindGameObjectWithTag(ConstValue.TAG_PLAYER).transform;

        Projectile projectile = ProjectileObjectPool.Instance.Pop(EProjectile.EYE_BULLET);
        
        if (animator.transform.position.x < m_playerTransform.position.x)
        {
            animator.transform.localScale = Vector3.one * 1.5f;
            projectile.Initialize(animator.transform.gameObject, false);
        }
        else if (animator.transform.position.x > m_playerTransform.position.x)
        {
            animator.transform.localScale = ConstValue.FLIP_SCALE * 1.5f;
            projectile.Initialize(animator.transform.gameObject, true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_resetTime <= 0)
        {
            m_randomValue = UnityEngine.Random.Range(0, 3);

            switch (m_randomValue)
            {
                case 0:
                    animator.SetTrigger(ConstValue.TRIGGER_IDLE);
                    break;
                case 1:
                    animator.SetTrigger(ConstValue.TRIGGER_SKILL3);
                    break;
                case 2:
                    animator.SetTrigger(ConstValue.TRIGGER_SKILL2);
                    break;
            }
        }
        else
        {
            m_resetTime -= Time.deltaTime;
        }
    }
}
