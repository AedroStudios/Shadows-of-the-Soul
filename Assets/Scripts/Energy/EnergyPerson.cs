using UnityEngine;
using Variables;

namespace Energy 
{
  public class EnergyPerson : EnergyComponent 
  {
    [SerializeField] private IntVariable _actualEnergy;
    [SerializeField] private IntVariable _giveEnergyAmount;
    [SerializeField] private FloatVariable _maxDistanceToGiveEnergy;
    [SerializeField] private GameObject _energyBallPrefab;

    public bool IsWithMaxEnergy => ActualEnergyValue >= MaxEnergyValue;
    private void Update()
    {
      _actualEnergy.Value = _actualEnergyAmount;
    }

    public void DropEnergy(int amount)
    {
      ActualEnergyValue -= amount;
      // TODO: Funcion que causa problemas de rendimiento lo de abajo Pasar a factory
      var newEnergyBall = Instantiate(_energyBallPrefab, transform.position, Quaternion.identity);
      newEnergyBall.GetComponent<EnergyBall>().ActualEnergyValue = amount;
    }
    // TODO: CREAR ACCION DEL PERSONAJE Y DE LOS NPCs DE ESTO.
    public void GiveEnergyToClosestPerson()
    {
      var closestColliders = Physics2D.OverlapCircleAll(transform.position, _maxDistanceToGiveEnergy.Value);
      if (closestColliders.Length == 0) return;
      var closestPerson = GetClosestPerson(closestColliders);
      if (!closestPerson ||closestPerson.IsWithMaxEnergy)
      {
        DropEnergy(_giveEnergyAmount.Value);
      }
      else GiveEnergy(_giveEnergyAmount.Value, closestPerson);
    }


    private EnergyPerson GetClosestPerson(Collider2D[] colliders)
    {
      EnergyPerson closestPerson = null;
      if (colliders.Length == 1)
      {
        return colliders[0].TryGetComponent(out closestPerson) ? closestPerson : null;
      }
      foreach (var collider in colliders) // TODO: Cambiar a for
      {
        collider.TryGetComponent(out EnergyPerson person);
        // Comprobaciones, coger el m�s cercano.
        closestPerson = person ? (!closestPerson ? person :
          Vector3.Distance(closestPerson.transform.position, transform.position) >
          Vector3.Distance(person.transform.position, transform.position) ? person : closestPerson) : closestPerson;
      }
      return closestPerson;
    }
  }
}