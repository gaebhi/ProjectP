using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileFx : MonoBehaviour
{
    public EProjectile Type;
    public float AnimationTime = 0.15f;

    private Animator m_animator = null;
    private const string TRIGGER = "Hit";

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Play()
    {
        m_animator.SetTrigger(TRIGGER);
    }
}
