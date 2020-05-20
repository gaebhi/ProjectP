using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CheckPhase : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("HP�� �Ʒ������� ���� �� ���� Phase�� �̵�")]
    public SharedFloat NextPhaseHp;

    public override TaskStatus OnUpdate()
	{
        if(transform.GetComponent<HealthComponent>().Hp <= NextPhaseHp.Value)
            return TaskStatus.Success;
        return TaskStatus.Failure;
	}
}