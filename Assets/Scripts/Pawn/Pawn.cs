using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Pawn : MonoBehaviour
{
    protected const float ACCELERATION = 20f;
    protected const float JUMP_POWER = 6f;
    protected const float MAX_SPEED = 5f;
    protected const float MIN_FLIP_SPEED = 0.1f;

    protected LayerMask GroundMask;

    protected const string STR_RUN = "RunningSpeed";
    protected const string STR_JUMP = "Jump";
    protected const string STR_GROUND = "Ground";

    protected readonly int m_runHash = Animator.StringToHash(STR_RUN);
    protected readonly int m_jumpHash = Animator.StringToHash(STR_JUMP);
    protected readonly int m_groundHash = Animator.StringToHash(STR_GROUND);

    protected readonly Vector3 m_flipScale = new Vector3(-1, 1, 1);

    protected PlayerController m_playerController = null;

    protected CapsuleCollider2D m_capsule = null;
    protected Rigidbody2D m_rigidbody = null;
    protected Collider2D m_collider = null;
    protected Animator m_animator = null;
    protected InputComponent m_inputComponet = null;

    protected Vector2 m_moveInput = Vector2.zero;
    protected bool m_jumpInput = false;

    protected bool m_bFlip = false;
    protected bool m_bJumping = false;
    protected bool m_bFalling = false;
    protected bool m_bGround = true;

    protected virtual void Awake()
    {
        m_capsule = GetComponent<CapsuleCollider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponent<Animator>();

        m_moveInput = Vector2.zero;
        m_jumpInput = false;

        m_bFlip = false;
        m_bJumping = false;
        m_bGround = true;

        GroundMask = LayerMask.GetMask(STR_GROUND);

        PostInitializeComponents();
    }

    private void updateDirection()
    {
        if (m_rigidbody.velocity.x > MIN_FLIP_SPEED && m_bFlip)
        {
            m_bFlip = false;
            transform.localScale = Vector3.one;
        }
        else if (m_rigidbody.velocity.x < -MIN_FLIP_SPEED && !m_bFlip)
        {
            m_bFlip = true;
            transform.localScale = m_flipScale;
        }
    }

    protected virtual void moveRight(float _axis)
    {
        if (_axis == 0)
            m_moveInput = Vector2.zero;
        if (_axis == -1)
            m_moveInput = Vector2.left;
        if (_axis == 1)
            m_moveInput = Vector2.right;
    }

    protected virtual void jump()
    {
        if (m_bJumping == false)
        {
            m_jumpInput = true;
        }
    }

    protected void FixedUpdate()
    {
        updateJump();
        updateGrounding();
        updateVelocity();
        updateDirection();
    }

    protected virtual void updateJump()
    {
        if (m_bJumping && m_rigidbody.velocity.y < 0)
            m_bFalling = true;

        if (m_jumpInput && m_bGround == true)
        {
            m_rigidbody.AddForce(Vector2.up * JUMP_POWER, ForceMode2D.Impulse);
            m_animator.SetTrigger(m_jumpHash);
            m_jumpInput = false;
            m_bJumping = true;
            //todo::Play audio
        }
        else if (m_bJumping && m_bFalling && m_bGround == true)
        {
            m_bJumping = false;
            m_bFalling = false;
            //todo::Play audio
        }
    }
    protected void updateGrounding()
    {
        if (m_collider.IsTouchingLayers(GroundMask))
        {
            m_bGround = true;
        }
        else
        {
            m_bGround = false;
        }
        m_animator.SetBool(m_groundHash, m_bGround);
    }
    protected virtual void updateVelocity()
    {
        Vector2 velocity = m_rigidbody.velocity;

        velocity += m_moveInput * ACCELERATION * Time.fixedDeltaTime;

        m_moveInput = Vector2.zero;

        //속도제한
        velocity.x = Mathf.Clamp(velocity.x, -MAX_SPEED, MAX_SPEED);
        m_rigidbody.velocity = velocity;

        float speedNormal = Mathf.Abs(velocity.x) / MAX_SPEED;

        m_animator.SetFloat(m_runHash, speedNormal);

        //todo::Play audio
    }

    public virtual void PostInitializeComponents()
    {
        
    }

    public virtual void PossessedBy(PlayerController _playerController)
    {
        m_playerController = _playerController;
        m_playerController.OnPossess(this);
    }

    public virtual void SetupPlayerInputComponent(InputComponent _inputComponent)
    {
        m_inputComponet = _inputComponent;
        m_inputComponet.MoveRight += moveRight;
        m_inputComponet.Jump += jump;
    }
}
