using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Á×À½")]
[TaskCategory("ProjectP")]

public class Death : Action
{
	public override void OnStart()
	{
        transform.GetComponent<Animator>().SetTrigger(ConstValue.TRIGGER_DEATH);
        transform.GetComponent<Rigidbody2D>().gravityScale = 0f;
        transform.GetComponent<Collider2D>().enabled = false;
    }

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Running;
	}
}