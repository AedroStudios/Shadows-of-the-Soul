using UnityEngine;
using Variables;

namespace Energy 
{
  public class PlayerEnergy : MonoBehaviour 
  {
    #region VALUES
    [SerializeField] private FloatVariable _maxStamina;
    [SerializeField] private FloatVariable _actualStamina;
    public float MaxStaminaValue => _maxStamina.Value;
    public float ActualStaminaValue
    {
      get => _actualStamina.Value;
      set
      {
        if (value > MaxStaminaValue) _actualStamina.Value = MaxStaminaValue;
        else if (value < 0) _actualStamina.Value = 0;
        else _actualStamina.Value = value;
      }
    }
    #endregion

    private void Awake()
    {
      ActualStaminaValue = MaxStaminaValue;
    }
  }
}