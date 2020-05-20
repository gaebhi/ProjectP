using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mage : Pawn
{
    private AttackComponent m_attack = null;
    private HealthComponent m_health = null;
    private ChaserComponent m_chaser = null;

    private bool m_bDash = false;
    private float m_dashSpeed = 10f;
    private const float m_dashDuration = 0.3f;
    private const float m_dashCoolTime = 1.5f;
    private bool m_bDashCoolTime = true;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(ConstValue.LAYER_PLAYER), LayerMask.NameToLayer(ConstValue.LAYER_ENEMY), true);

        m_attack = GetComponent<AttackComponent>();
        m_chaser = GetComponent<ChaserComponent>();
        m_health = GetComponent<HealthComponent>();
        m_health.UpdataHp += updateHp;
    }

    public override void SetupPlayerInputComponent(InputComponent _inputComponent)
    {
        base.SetupPlayerInputComponent(_inputComponent);

        ((MageInputComponent)m_inputComponet).Attack += attack;
        ((MageInputComponent)m_inputComponet).Dash += dash;
        ((MageInputComponent)m_inputComponet).ChaserAttack += chaserAttack;
    }

    protected override void updateVelocity()
    {
        Vector2 velocity = m_rigidbody.velocity;

        if (m_bDash)
        {
            velocity.x = Vector2.right.x * m_dashSpeed;
            if (m_bFlip)
                velocity.x = -velocity.x;
            m_rigidbody.velocity = velocity;
        }
        else
        {
            velocity += m_moveInput * ACCELERATION * Time.fixedDeltaTime;
            velocity.x = Mathf.Clamp(velocity.x, -MAX_SPEED, MAX_SPEED);
            m_rigidbody.velocity = velocity;
        }

        m_moveInput = Vector2.zero;

        float speedNormal = Mathf.Abs(velocity.x) / MAX_SPEED;

        m_animator.SetFloat(m_runHash, speedNormal);

        //todo::Play audio
    }
    private void attack()
    {
        m_attack.Do(m_animator,m_bFlip);
    }

    private void chaserAttack()
    {
        m_chaser.Do(m_animator);
    }

    private void dash()
    {
        if (m_bDashCoolTime == false)
            return;

        m_bDash = true;
        m_animator.SetTrigger(ConstValue.TRIGGER_DASH);

        m_bDashCoolTime = false;

        DOVirtual.DelayedCall(m_dashDuration, () => { m_bDash = false; }).SetTarget(transform);
        DOVirtual.DelayedCall(m_dashCoolTime, () => { m_bDashCoolTime = true; }).SetTarget(transform);
    }

    private void updateHp(float _hp)
    {
        UIManager.Instance.SetMageHp(m_health.Hp / m_health.MaxHP);

        if (_hp <= 0)
        {
            m_animator.SetTrigger(ConstValue.TRIGGER_DEATH);
            m_rigidbody.gravityScale = 0f;
            m_collider.enabled = false;

            m_inputComponet.DeleteEventAll();
        }
        else
        {
            m_animator.SetTrigger(ConstValue.TRIGGER_HURT);
        }
    }
}
