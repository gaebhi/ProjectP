using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Laser: MonoBehaviour
{

    public Transform characterTrasform = null;
    public LineRenderer DangerLine = null;
    public LineRenderer LaserLine = null;

    private float m_dangerWidth = 0.3f;
    private float m_dangerDuration = 1f;

    private float m_laserWidth = 0.5f;
    private float m_laserDuration = 0.5f;

    public void Shoot()
    {
        drawDangerLine();
    }

    public void Update()
    {
        Debug.DrawRay(transform.position, Vector2.right * 100f);
    }
    private void drawDangerLine()
    {
        DOTween.To(() => m_dangerWidth, (float _value) =>
        {
            DangerLine.startWidth = _value;
        }, 0f, m_dangerDuration)
        .SetTarget(transform)
        .OnStart(() =>
        {
            DangerLine.enabled = true;
        })
        .OnComplete(() =>
        {
            DangerLine.enabled = false;
            DangerLine.startWidth = m_dangerWidth;
            drawLaserLine();
        });
    }

    private void drawLaserLine()
    {
        DOTween.To(() => m_laserWidth, (float _value) =>
        {
            LaserLine.startWidth = _value;
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("hit");
                }
            }

        }, 0f, m_laserDuration)
        .SetTarget(transform)
        .OnStart(() =>
        {
            LaserLine.enabled = true;
        })
        .OnComplete(() =>
        {
            LaserLine.enabled = false;
            LaserLine.startWidth = m_laserWidth;
        });
    }
}
