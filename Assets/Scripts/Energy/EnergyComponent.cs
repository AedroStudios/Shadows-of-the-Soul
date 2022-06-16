using UnityEngine;
using Variables;

namespace Energy
{
  public abstract class EnergyComponent : MonoBehaviour
  {
    #region VALUES
    [SerializeField] private IntVariable _maxEnergy;
    [SerializeField] protected int _actualEnergyAmount;
    public int MaxEnergyValue => _maxEnergy.Value;
    public int ActualEnergyValue
    {
      get => _actualEnergyAmount;
      set
      {
        if (value > MaxEnergyValue) _actualEnergyAmount = MaxEnergyValue;
        else if (value < 0) _actualEnergyAmount = 0;
        else _actualEnergyAmount = value;
      }
    }
    #endregion
    private void Awake()
    {
      ActualEnergyValue = MaxEnergyValue;
    }

    protected void GiveEnergy(int amount, EnergyComponent receptor)
    {
      int excessEnergy = 0;
      int totalEnergy = receptor.ActualEnergyValue + amount;
      
      if (totalEnergy > receptor.MaxEnergyValue) 
        excessEnergy = totalEnergy - receptor.MaxEnergyValue;
      
      amount -= excessEnergy;
      if (amount <= 0 || amount > ActualEnergyValue) return;
      
      ActualEnergyValue -= amount;
      receptor.ReceiveEnergy(amount);
    }
    private void ReceiveEnergy(int amount)
    {
      ActualEnergyValue += amount;
    }
  }
}