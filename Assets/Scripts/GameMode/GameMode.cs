using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    protected PlayerController m_playerController = null;
    protected Pawn m_pawn = null;

    protected virtual void Awake()
    {
        m_pawn = FindObjectOfType<Pawn>();
        m_playerController = FindObjectOfType<PlayerController>();

        PostLogin();
    }

    public  virtual void PostLogin()
    {
        m_pawn.PossessedBy(m_playerController);
    }
}
