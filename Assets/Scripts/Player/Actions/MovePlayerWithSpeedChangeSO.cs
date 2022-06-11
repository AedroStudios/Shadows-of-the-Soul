using UnityEngine;
using FSM;
using FSM.ScriptableObjects;

[CreateAssetMenu(fileName = "MovePlayerWithSpeedChange", menuName = "Player/Actions/Move Player With Speed Change")]
public class MovePlayerWithSpeedChangeSO : StateActionSO<MovePlayerWithSpeedChange>
{
  [Range(0, 2f), Tooltip("Cambia la velocidad de movimiento disponible por un factor")]
  public float speedMultiplier = 1f;
}

public class MovePlayerWithSpeedChange : StateAction
{
	protected new MovePlayerWithSpeedChangeSO OriginSO => (MovePlayerWithSpeedChangeSO)base.OriginSO;
  float SpeedMultiplier { get => OriginSO.speedMultiplier; }
  PlayerController controller;
  public override void Awake(StateMachine stateMachine)
	{
    controller = stateMachine.GetComponent<PlayerController>();
  }

  public override void OnFixedUpdate()
  {
    controller.Run(multiplier: SpeedMultiplier);
  }
}
