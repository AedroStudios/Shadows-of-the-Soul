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
  InputReader Input { get => OriginSO.inputReader; }
  bool isStealthPressed;

  public override void Awake(StateMachine stateMachine)
	{
    Input.StealthEvent += () => { isStealthPressed = true; };
	}
	
	protected override bool Statement()
	{
    if (isStealthPressed)
    {
      isStealthPressed = false; // Clean cache
      return true;
    }

    return false;
	}
}
