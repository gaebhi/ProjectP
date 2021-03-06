﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsySkill3B : StateMachineBehaviour
{
    private int m_randomValue;

    private float m_resetTime = 1.8f;
    private float m_speed = 3.8f;

    private Transform m_playerTransform;
    private Vector2 m_targetPosition;
    private float m_direction;

    private PsyBehaviorComponent m_attack = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_playerTransform = GameObject.FindGameObjectWithTag(ConstValue.TAG_PLAYER).transform;

        if (animator.transform.position.x < m_playerTransform.position.x)
        {
            animator.transform.localScale = Vector3.one * 1.5f;
            m_direction = 1f;
        }
        else if (animator.transform.position.x > m_playerTransform.position.x)
        {
            animator.transform.localScale = ConstValue.FLIP_SCALE * 1.5f;
            m_direction = -1f;
        }
        m_attack = animator.GetComponent<PsyBehaviorComponent>();
        
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
                    animator.SetTrigger(ConstValue.TRIGGER_SKILL2);
                    break;
                case 2:
                    animator.SetTrigger(ConstValue.TRIGGER_SKILL3);
                    break;
            }
        }
        else
        {
            m_resetTime -= Time.deltaTime;
        }

        m_attack.RevolveAttack();

        m_targetPosition.x = animator.transform.position.x + m_direction;
        m_targetPosition.y = animator.transform.position.y;

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_targetPosition, m_speed * Time.deltaTime);
    }
}
