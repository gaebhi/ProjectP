using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage01 : GameMode
{
    protected override void Awake()
    {
        base.Awake();
        //todo::각 스테이지에 맞는 준비 ex)데이터 로드
    }
    public override void PostLogin()
    {
        m_pawn.PossessedBy(m_playerController);
    }
}
