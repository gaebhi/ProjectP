using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChaserComponent : ActionComponent
{
    public Transform ShootTransform = null;
    private GameObject m_Target = null;

    private float m_coolTime = 0.35f;
    private int m_shootDirection = -1;
    private const float m_attackRange = 5f;


    private const float m_minJumpPower = 1f;
    private const float m_maxJumpPower = 2f;
    private const float m_duration = 0.3f;

    private const float m_objectPoolTime = 5f;

    private bool m_bCoolTime = true;

    private const string TAG_ENEMY = "Enemy";

    public override void Do(Animator _animator, bool bFlip = false)
    {
        if (m_bCoolTime == false)
            return;

        m_Target = GameObject.FindGameObjectWithTag(TAG_ENEMY);

        if (m_Target == null)
            return;

        if (Vector2.Distance(m_Target.transform.position, transform.position) > m_attackRange)
            return;

        m_shootDirection = m_shootDirection *-1; 

        Projectile projectile = ProjectileObjectPool.Instance.Pop(EProjectile.CHASER);

        if (projectile != null)
        {
            projectile.Initialize(transform.gameObject);
            projectile.transform.position = ShootTransform.position;
            projectile.transform.DOJump(m_Target.transform.position, UnityEngine.Random.Range(m_minJumpPower, m_maxJumpPower) * m_shootDirection, 0, m_duration)
                .SetEase(Ease.Linear)
                .OnStart(() =>
                {
                    projectile.TrailTransform.gameObject.SetActive(true);
                })  
                .OnComplete(() =>
                {
                    projectile.TrailTransform.gameObject.SetActive(false);
                    projectile.transform.gameObject.SetActive(false);
                    projectile.transform.position = ShootTransform.position;
                });
        }

        m_bCoolTime = false;
        DOVirtual.DelayedCall(m_coolTime, () => { m_bCoolTime = true; }).SetTarget(transform);
    }
}
