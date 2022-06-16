using UnityEngine;
using Energy;

namespace NPCs 
{
  public class NPC : MonoBehaviour
  {
    private float _timer;

    private void Awake()
    {
      _timer = 6;
    }

    private void Update() // TESTING
    {
      _timer -= Time.deltaTime;
      if (_timer < 0)
      {
        _timer = 6;
        GetComponent<EnergyPerson>().GiveEnergyToClosestPerson();
      }
    }
  }
}