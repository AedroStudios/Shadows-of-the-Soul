using Events;
using Events.ScriptableObjects;
using UnityEngine;
using FSM;
using FSM.ScriptableObjects;

[CreateAssetMenu(fileName = "TryToFree", menuName = "Player/Actions/Try To Free")]
public class TryToFreeSO : StateActionSO<TryToFree>
{
  [SerializeField] private Input.InputReader input;
  [SerializeField] private VoidEventSO playerReleasedEvent;

  public int resistance = 4;
  public int resistanceRuntime;

  public int freePower = 1;
  private void OnEnable()
  {
    input.LetGoIfCaughtEvent += ReduceResistance;
  }

  private void OnDisable()
  {
    input.LetGoIfCaughtEvent -= ReduceResistance;
  }

  private void ReduceResistance()
  {
    resistanceRuntime -= freePower;

    if (resistanceRuntime <= 0)
    {
      (playerReleasedEvent as IGameEvent).RaiseEvent();
    }
  }
}

public class TryToFree : StateAction
{
  private new TryToFreeSO OriginSO => (TryToFreeSO)base.OriginSO;

  public override void Awake(StateMachine stateMachine)
  {
  }

  public override void OnStateEnter()
  {
    OriginSO.resistanceRuntime = OriginSO.resistance;
  }

  public override void OnStateExit()
  {
    OriginSO.resistanceRuntime = 0; // Si sale del estado agarrado de otra manera que no sea presionando el boton
  }
}