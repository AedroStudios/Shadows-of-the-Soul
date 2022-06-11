using UnityEngine;
using FSM;
using FSM.ScriptableObjects;

[CreateAssetMenu(fileName = "IsPlayerMoving", menuName = "Player/Conditions/Is Player Moving")]
public class IsPlayerMovingSO : StateConditionSO<IsPlayerMoving>
{
}

public class IsPlayerMoving : Condition
{
	protected new IsPlayerMovingSO OriginSO => (IsPlayerMovingSO)base.OriginSO;
  PlayerController controller;
	public override void Awake(StateMachine stateMachine)
	{
    controller = stateMachine.GetComponent<PlayerController>();
  }
	
	protected override bool Statement()
	{
		return controller.MoveInput.x != 0;
	}
}
