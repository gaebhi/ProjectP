using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstValue
{
    public static readonly Vector3 FLIP_SCALE = new Vector3(-1, 1, 1);

    public const string TAG_PLAYER = "Player";

    public const string PATH_PROJECTILE = "Projectiles/";

    public const string TRIGGER_IDLE = "Idle";
    
    public const string TRIGGER_ATTACK = "Attack";
    public const string TRIGGER_JUMP = "Jump";

    public const string TRIGGER_PHASE_01 = "Phase01";
    public const string TRIGGER_PHASE_02 = "Phase02";

    public const string TRIGGER_DEATH = "Death";

    public const string TRIGGER_HURT = "Hurt";

    public const string TRIGGER_SKILL = "Skill";
    public const string TRIGGER_SKILL2 = "Skill2";
    public const string TRIGGER_SKILL3 = "Skill3";

    public const string RES_DEFAUT_BULLET = "Default";
    public const string RES_EYE_BULLET = "EyeBullet";
    public const string RES_MAGE_BULLET = "MageBullet";
    public const string RES_CHASER_BULLET = "ChaserBullet";
}
