using UnityEngine;
using FSM;
using FSM.ScriptableObjects;

[CreateAssetMenu(fileName = "Jump", menuName = "Player/Actions/Jump")]
public class JumpSO : StateActionSO<Jump>
{
}

public class Jump : StateAction
{
	protected new JumpSO OriginSO => (JumpSO)base.OriginSO;
  PlayerController controller;

	public override void Awake(StateMachine stateMachine)
	{
    controller = stateMachine.GetComponent<PlayerController>();
  }
	
	public override void OnStateEnter()
	{
    controller.Jump();
	}
	
	public override void OnStateExit()
	{
	}
}
