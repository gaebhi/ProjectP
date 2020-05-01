using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    public event System.Action<float> MoveRight;
    public event System.Action Jump;

    protected virtual void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.A))
        {
            if(MoveRight != null)
                MoveRight(-1);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D))
        {
            if (MoveRight != null)
                MoveRight(+1);
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
        {
            if (Jump != null)
                Jump();
        }
    }

    public virtual void DeleteEventAll()
    {
        MoveRight = null;
        Jump = null;
    }
}
