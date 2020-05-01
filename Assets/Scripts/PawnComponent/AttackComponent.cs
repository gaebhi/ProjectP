using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AttackComponent : ActionComponent
{
    private const float m_distance = 5f;
    private const float m_duration = 1f;
    private const float m_coolTime = 0.5f;
    private bool m_bCoolTime = true;

    private const string STR_TRIGGER = "Attack";
    private readonly int m_hash = Animator.StringToHash(STR_TRIGGER);

    public override void Do(Animator _animator, bool _bFlip)
    {
        if (m_bCoolTime == false)
            return;

        _animator.SetTrigger(m_hash);

        Projectile projectile = ProjectileObjectPool.Instance.Pop(EProjectile.MAGE_BULLET);
        projectile.Initialize(transform.gameObject,_bFlip);

        m_bCoolTime = false;
        DOVirtual.DelayedCall(m_coolTime, () => { m_bCoolTime = true; }).SetTarget(transform);
    }
}
