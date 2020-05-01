using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Pawn m_pawn = null;
    protected InputComponent m_inputComponet = null;

    public virtual void OnPossess(Pawn _pawn)
    {
        m_pawn = _pawn;

        m_inputComponet = FindObjectOfType<InputComponent>();

        m_pawn.SetupPlayerInputComponent(m_inputComponet);

    }
}
