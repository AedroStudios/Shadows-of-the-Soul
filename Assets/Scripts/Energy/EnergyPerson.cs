using UnityEngine;
using Variables;

namespace Energy 
{
  public class EnergyPerson : EnergyComponent 
  {

    [SerializeField] private IntVariable _actualEnergy;
    [SerializeField] private IntVariable _giveEnergyAmount;
    [SerializeField] private FloatVariable _maxDistanceToGiveEnergy;

    private void Update()
    {
      _actualEnergy.Value = _actualEnergyAmount;
    }

    public void DropEnergy(int amount)
    {
      ActualEnergyValue -= amount; // Crear bola de energía con la energía soltada
    }
    // CREAR ACCION DEL PERSONAJE Y DE LOS NPCs DE ESTO.
    public void GiveEnergyToClosestPerson()
    {
      var closestColliders = Physics2D.OverlapCircleAll(transform.position, _maxDistanceToGiveEnergy.Value);
      if (closestColliders.Length == 0) return;
      var closestPerson = GetClosestPerson(closestColliders);
      if (closestPerson == null) DropEnergy(_giveEnergyAmount.Value);
      else GiveEnergy(_giveEnergyAmount.Value, closestPerson);
    }
    private EnergyPerson GetClosestPerson(Collider2D[] colliders)
    {
      EnergyPerson closestPerson = null;
      if (colliders.Length == 1)
      {
        if (colliders[0].TryGetComponent(out closestPerson)) return closestPerson;
        else return null;
      }
      foreach (var collider in colliders)
      {
        collider.TryGetComponent(out EnergyPerson person);
        // Comprobaciones, coger el más cercano.
        closestPerson = person ? (!closestPerson ? person :
          Vector3.Distance(closestPerson.transform.position, transform.position) >
          Vector3.Distance(person.transform.position, transform.position) ? person : closestPerson) : null;
      }
      return closestPerson;
    }
  }
}