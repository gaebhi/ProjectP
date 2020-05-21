using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    public EProjectile Type;
    public Transform TrailTransform = null;

    public float Damage = 0f;
    public float BulletSpeed = 8f;
    public float LifeTime = 5f;

    private readonly Vector3 m_flipScale = new Vector3(-1, 1, 1);

    private GameObject m_owner = null;
    private Vector3 m_diection = Vector3.zero;

    private const float m_objectPoolTime = 5f;

    private void Update()
    {
        transform.Translate(m_diection * BulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthComponent>() != null)
        {
            if (collision.gameObject == m_owner)
                return;

            collision.GetComponent<HealthComponent>().TakeDamage(Damage);
            
            ProjectileFx fx =  ProjectileFxObjectPool.Instance.Pop(Type);

            if (fx != null)
            {
                fx.transform.position = transform.position;
                fx.Play();
                DOVirtual.DelayedCall(fx.AnimationTime, () =>
                {
                    ProjectileFxObjectPool.Instance.Push(fx);
                });
            }

            ProjectileObjectPool.Instance.Push(this);
            transform.gameObject.SetActive(false);

            if(TrailTransform != null)
                TrailTransform.gameObject.SetActive(false);

        }
    }

    public void Initialize(GameObject _owner, bool _bFlip = false)
    {
        m_owner = _owner;
        if (_bFlip)
        {
            m_diection = -transform.right;
            transform.localScale = m_flipScale;
        }
        else
        {
            m_diection = transform.right;
            transform.localScale = Vector3.one;
        }
        transform.position = _owner.transform.position;
        transform.gameObject.SetActive(true);

        //충돌하지 않은 경우는 여기서 처리
        DOVirtual.DelayedCall(LifeTime, () => 
        {
            if(transform.gameObject.activeInHierarchy)
                ProjectileObjectPool.Instance.Push(this);
        }); 
    }
}
