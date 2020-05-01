using UnityEngine;

public abstract class ActionComponent : MonoBehaviour
{
    public abstract void Do(Animator _animator, bool _bFlip = false);
}
