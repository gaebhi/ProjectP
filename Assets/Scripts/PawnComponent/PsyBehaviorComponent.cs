﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsyBehaviorComponent : MonoBehaviour
{
    public LayerMask AttackMask;

    private float m_normalRange = 1f;
    private float m_normalDamage = 5f;

    private float m_attacklRange = 1.2f;
    private float m_attackDamage = 10f;

    private Animator m_animator = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Idle()
    {
        if(m_animator != null)
            m_animator.SetTrigger(ConstValue.TRIGGER_IDLE);
    }

    public void Run()
    {
        if (m_animator != null)
            m_animator.SetTrigger(ConstValue.TRIGGER_RUN);
    }

    /// <summary>
    /// 평타 공격
    /// </summary>
    public void Attack()
    {
        if (m_animator != null)
            m_animator.SetTrigger(ConstValue.TRIGGER_ATTACK);
    }

    public void AttackEvent(AnimationEvent _event)
    {
        Vector3 attackPosition = transform.position;

        Collider2D collider = Physics2D.OverlapCircle(attackPosition, m_normalRange, AttackMask);

        if (collider != null)
        {
            if (collider.gameObject != transform.gameObject && collider.GetComponent<HealthComponent>() != null)
                collider.GetComponent<HealthComponent>().TakeDamage(m_normalDamage);
        }
    }

    /// <summary>
    /// 회전 공격
    /// </summary>
    public void Revolve()
    {
        if (m_animator != null)
            m_animator.SetTrigger(ConstValue.TRIGGER_SKILL3);
    }

    /// <summary>
    /// 회전 공격 판정
    /// </summary>
    public void RevolveAttack()
    {
        Vector3 attackPosition = transform.position;
        
        Collider2D collider = Physics2D.OverlapCircle(attackPosition, m_attacklRange, AttackMask);

        if (collider != null)
        {
            if (collider.gameObject != transform.gameObject)
                if(collider.GetComponent<HealthComponent>() != null)
                    collider.GetComponent<HealthComponent>().TakeDamage(m_attackDamage);
        }
    }

    /// <summary>
    /// 발사체 공격
    /// </summary>
    public void EyeSkill(Transform _target)
    {
        if(m_animator != null)
            m_animator.SetTrigger(ConstValue.TRIGGER_SKILL2);

        Projectile projectile = ProjectileObjectPool.Instance.Pop(EProjectile.EYE_BULLET);

        if (transform.position.x < _target.position.x)
        {
            transform.localScale = Vector3.one * 1.5f;
            projectile.Initialize(transform.gameObject, false);
        }
        else if (transform.position.x > _target.position.x)
        {
            transform.localScale = ConstValue.FLIP_SCALE * 1.5f;
            projectile.Initialize(transform.gameObject, true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_normalRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attacklRange);
    }
}
