using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psychic : MonoBehaviour
{
    private Collider2D m_collider = null;
    private Animator m_animator = null;
    private Rigidbody2D m_rigidbody = null;
    private HealthComponent m_health = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_health = GetComponent<HealthComponent>();

        m_health.UpdataHp += updateHp;
    }

    private void updateHp(float _hp)
    {
        if (_hp <= 0)
        {
            m_animator.SetTrigger(ConstValue.TRIGGER_DEATH);
            m_rigidbody.gravityScale = 0f;
            m_collider.enabled = false;
        }

        if (m_health.Hp < 50)
        {
            m_animator.SetTrigger(ConstValue.TRIGGER_PHASE_02);
        }
        if (m_health.Hp < 80)
        {
            m_animator.SetTrigger(ConstValue.TRIGGER_PHASE_01);
        }

        UIManager.Instance.SetPsyHp(m_health.Hp / m_health.MaxHP);
    }
}

