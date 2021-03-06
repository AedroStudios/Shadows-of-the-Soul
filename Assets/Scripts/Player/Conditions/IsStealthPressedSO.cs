using UnityEngine;
using FSM;
using FSM.ScriptableObjects;
using Input;

[CreateAssetMenu(fileName = "IsStealthPressed", menuName = "Player/Conditions/Is Stealth Pressed")]
public class IsStealthPressedSO : StateConditionSO<IsStealthPressed>
{
  public InputReader inputReader;
}

public class IsStealthPressed : Condition
{
  protected new IsStealthPressedSO OriginSO => (IsStealthPressedSO)base.OriginSO;
  private InputReader Input { get => OriginSO.inputReader; }
  private bool _isStealthPressed;

  public override void Awake(StateMachine stateMachine)
	{
    Input.StealthEvent += () => { _isStealthPressed = true; };
	}
	
	protected override bool Statement()
	{
    if (!_isStealthPressed)
    {
      return false;
    }

    _isStealthPressed = false; // Clean cache
    return true;

  }
}
