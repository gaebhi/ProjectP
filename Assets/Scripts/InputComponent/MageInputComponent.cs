using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageInputComponent : InputComponent
{
    public event System.Action Attack;
    public event System.Action Dash;
    public event System.Action ChaserAttack;

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKey(KeyCode.K))
        {
            if(Attack != null)
                Attack();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
        {
            if (Dash != null)
                Dash();
        }
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKey(KeyCode.L))
        {
            if (ChaserAttack != null)
                ChaserAttack();
        }
    }

    public override void DeleteEventAll()
    {
        base.DeleteEventAll();
        Attack = null;
        Dash = null;
        ChaserAttack = null;
    }
}
