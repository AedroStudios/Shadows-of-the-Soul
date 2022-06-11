using UnityEngine;
using FSM;
using FSM.ScriptableObjects;

[CreateAssetMenu(fileName = "IsPlayerJumping", menuName = "Player/Conditions/Is Player Jumping")]
public class IsPlayerJumpingSO : StateConditionSO<IsPlayerJumping>
{
}

public class IsPlayerJumping : Condition
{
	protected new IsPlayerJumpingSO OriginSO => (IsPlayerJumpingSO)base.OriginSO;
  PlayerController controller;
	public override void Awake(StateMachine stateMachine)
	{
    controller = stateMachine.GetComponent<PlayerController>();
	}
	
	protected override bool Statement()
	{
		return controller.IsJumping;
	}
}
