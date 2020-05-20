using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CheckPhase : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("HP가 아랫값보다 작을 때 다음 Phase로 이동")]
    public SharedFloat NextPhaseHp;

    public override TaskStatus OnUpdate()
	{
        if(transform.GetComponent<HealthComponent>().Hp <= NextPhaseHp.Value)
            return TaskStatus.Success;
        return TaskStatus.Failure;
	}
}