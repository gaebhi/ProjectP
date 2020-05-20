using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthComponent : MonoBehaviour
{
    public event System.Action<float> UpdataHp = null;

    public float MaxHP = 100f;

    private float m_hp = 100f;

    private float m_coolTime = 1f;
    private bool m_bCoolTime = false;

    public float Hp
    {
        get
        {
            return m_hp;
        }
        set
        {

        }
    }

    private bool m_bDead;

    public bool IsDead
    {
        get
        {
            return m_bDead;
        }
        set
        {

        }
    }

    public void TakeDamage(float _value)
    { 
        if (m_bDead || m_bCoolTime)
            return;

        m_hp -= _value;

        if (m_hp <= 0)
        {
            m_bDead = true;
        }

        m_bCoolTime = true;
        DOVirtual.DelayedCall(m_coolTime, () => { m_bCoolTime = false; });

        UpdataHp(m_hp);
    }
}
