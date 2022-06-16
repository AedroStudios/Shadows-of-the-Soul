using UnityEngine;

namespace CollisionsDetection 
{
  public class TriggererObject : MonoBehaviour 
  {
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.TryGetComponent<ITriggerable>(out var collisionableObject)) collisionableObject.ShotTrigger(GetComponent<Collider2D>());
    }
  }
}