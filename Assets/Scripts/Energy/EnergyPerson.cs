using System.Collections.Generic;
using System.Linq;
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
      var closestPerson = TryGetClosestPerson(closestColliders);
      if (!closestPerson || closestPerson.IsWithMaxEnergy)
      {
        DropEnergy(_giveEnergyAmount.Value);
      }
      else GiveEnergy(_giveEnergyAmount.Value, closestPerson);
    }

    #region Cache
    private readonly List<EnergyPerson> _energyCharacters = new();
    #endregion

    private EnergyPerson TryGetClosestPerson(IEnumerable<Collider2D> colliders)
    {
      _energyCharacters.Clear();
      GetAllEnergyComponents();

      if (_energyCharacters.Count == 0)
        return null;
      
      return _energyCharacters.Count == 1 ? 
          _energyCharacters.First() : 
          GetClosestPerson();

      void GetAllEnergyComponents()
      {
        foreach (Collider2D component in colliders)
        {
          if (TryGetEnergyComponent(component, out EnergyPerson person))
            _energyCharacters.Add(person);
        }
      }

      EnergyPerson GetClosestPerson()
      {
        var closestPerson = _energyCharacters.First();

        foreach (var person in _energyCharacters) // TODO: Cambiar a for
        {
          var distanceToClosestPerson = Vector3.Distance(closestPerson.transform.position, transform.position);
          var distanceToCurrentPerson = Vector3.Distance(person.transform.position, transform.position);
          if (distanceToClosestPerson > distanceToCurrentPerson)
          {
            closestPerson = person;
          }
        }
        return closestPerson;
      }
    }

    private static bool TryGetEnergyComponent(Component component, out EnergyPerson person)
    {
      return component.TryGetComponent(out person);
    }
  }
}