using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Pawn
{
    private AttackComponent m_attack = null;
    private DashComponent m_dash = null;
    private HealthComponent m_health = null;
    private ChaserComponent m_chaser = null;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(ConstValue.LAYER_PLAYER), LayerMask.NameToLayer(ConstValue.LAYER_ENEMY), true);
        m_attack = GetComponent<AttackComponent>();
        m_dash = GetComponent<DashComponent>();
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
        m_dash.Do(m_animator, m_bFlip);
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
