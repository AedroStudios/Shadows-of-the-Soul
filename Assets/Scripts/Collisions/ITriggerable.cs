using UnityEngine;

namespace CollisionsDetection 
{
  public interface ITriggerable 
  {
    void ShotTrigger(Collider2D collider); 
  }
}