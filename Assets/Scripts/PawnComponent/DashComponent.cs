using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DashComponent : ActionComponent
{
    private const float m_distance = 15f;
    private const float m_duration = 1f;
    private const float m_coolTime = 1f;
    private bool m_bCoolTime = true;

    private const string STR_TRIGGER= "Dash";
    private readonly int m_hash = Animator.StringToHash(STR_TRIGGER);

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
    public override void Do(Animator _animator,bool _bFlip = false)
    {
        if (m_bCoolTime == false)
            return;

        _animator.SetTrigger(m_hash);

        float direction = 1;
        if (_bFlip)
            direction = -1;

        m_bCoolTime = false;

        DOVirtual.DelayedCall(m_coolTime, () => { m_bCoolTime = true; }).SetTarget(transform);
        Vector2 velocity = m_rigidBody.velocity;
        velocity.x = Vector2.right.x * direction * m_distance;
        m_rigidBody.velocity = velocity;
        Debug.Log(m_rigidBody.velocity.x);
        //transform.DOMoveX(transform.position.x + direction * m_distance, m_duration)
        //    .OnStart(() =>
        //    {
        //        m_bCoolTime = false;
        //    })
        //    .OnComplete(() =>
        //    {
        //        DOVirtual.DelayedCall(m_coolTime, () => { m_bCoolTime = true; })
        //        .SetTarget(transform);
        //    });
    }
}
