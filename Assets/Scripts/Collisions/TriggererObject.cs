using UnityEngine;

namespace CollisionsDetection 
{
  public class TriggererObject : MonoBehaviour 
  {
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.TryGetComponent<ITriggerable>(out ITriggerable collisionObject)) 
        collisionObject.ShotTrigger(GetComponent<Collider2D>());
    }
  }
}