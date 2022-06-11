using UnityEngine;
using FSM;
using FSM.ScriptableObjects;

[CreateAssetMenu(fileName = "IsPlayerGrounded", menuName = "Player/Conditions/Is Player Grounded")]
public class IsPlayerGroundedSO : StateConditionSO<IsPlayerGrounded>
{
}

public class IsPlayerGrounded : Condition
{
	protected new IsPlayerGroundedSO OriginSO => (IsPlayerGroundedSO)base.OriginSO;
  PlayerController controller;
	public override void Awake(StateMachine stateMachine)
	{
    controller = stateMachine.GetComponent<PlayerController>();
	}
	
	protected override bool Statement()
	{
    return controller.LastOnGroundTime > 0;
  }
}
