using UnityEngine;
using Energy;
using System;

namespace NPCs 
{
  public class NPC : MonoBehaviour
  {
    private float _timer;
    private EnergyPerson _energyPerson;

    private void Awake()
    {
      _timer = 6;
    }

    private void Start()
    {
      _energyPerson = GetComponent<EnergyPerson>();
    }

    private void Update() // TESTING
    {
      _timer -= Time.deltaTime;
      if (_timer < 0)
      {
        _timer = 6;
        _energyPerson.GiveEnergyToClosestPerson();
      }
    }
  }
}