using UnityEngine;

public abstract class ActionComponent : MonoBehaviour
{
    protected Rigidbody2D m_rigidBody = null;
    public abstract void Do(Animator _animator, bool _bFlip = false);
}
