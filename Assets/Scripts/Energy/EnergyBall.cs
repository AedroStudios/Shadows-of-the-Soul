using UnityEngine;
using CollisionsDetection;

namespace Energy
{
  public class EnergyBall : EnergyComponent, ITriggerable
  {
    public void ShotTrigger(Collider2D collider)
    {
      GiveEnergyToPerson(collider.GetComponent<EnergyPerson>());
    }
    private void GiveEnergyToPerson(EnergyPerson person)
    {
      GiveEnergy(_actualEnergyAmount, person);
      // TODO: Hacer pool con los objetos de este tipo y añadirlo al pool, de mientras utilizo Destroy().
      if (_actualEnergyAmount <= 0) Destroy(gameObject);
    }
  }
}