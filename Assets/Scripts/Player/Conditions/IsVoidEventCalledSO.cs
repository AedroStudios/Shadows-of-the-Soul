using Events.ScriptableObjects;
using UnityEngine;
using FSM;
using FSM.ScriptableObjects;

[CreateAssetMenu(fileName = "IsVoidEventCalled", menuName = "Player/Conditions/Is Void Event Called")]
public class IsVoidEventCalledSO: StateConditionSO<IsVoidEventCalled>
{
  [SerializeField] private VoidEventSO playerCaughtEvent;

  [field: SerializeField]
  public bool IsEventCalled { get; set; }

  private void OnEnable()
  {
    playerCaughtEvent.OnEventRaised += OnEventRaised;
  }

  private void OnDisable()
  {
    playerCaughtEvent.OnEventRaised -= OnEventRaised;
  }

  private void OnEventRaised()
  {

    IsEventCalled = true;
  }
}

public class IsVoidEventCalled : Condition
{
  private new IsVoidEventCalledSO OriginSO => (IsVoidEventCalledSO)base.OriginSO;

  private bool IsEventCalled
  {
    get => OriginSO.IsEventCalled;
    set => OriginSO.IsEventCalled = value;
  }

  public override void Awake(StateMachine stateMachine)
  {
  }

  protected override bool Statement()
  {
    if (!IsEventCalled)
    {
      return false;
    }
    
    IsEventCalled = false;
    return true;
  }

}