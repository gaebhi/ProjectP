using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsyAttackComponent : MonoBehaviour
{
    public LayerMask AttackMask;

    private float m_normalRange = 0.8f;
    private float m_normalDamage = 5f;

    private float m_attacklRange = 1f;
    private float m_attackDamage = 10f;

    /// <summary>
    /// 평타 공격
    /// </summary>
    public void Attack()
    {
        Vector3 attackPosition = transform.position;

        Collider2D collider = Physics2D.OverlapCircle(attackPosition, m_normalRange);
        if (collider != null)
        {
            if (collider.gameObject != transform.gameObject && collider.GetComponent<HealthComponent>() != null)
                        collider.GetComponent<HealthComponent>().TakeDamage(m_normalDamage);
        }
    }

    /// <summary>
    /// 회전 공격
    /// </summary>
    public void Skill03()
    {
        Vector3 attackPosition = transform.position;
        
        Collider2D collider = Physics2D.OverlapCircle(attackPosition, m_attacklRange);
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
    public void EyeAttack()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_normalRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attacklRange);
    }
}
