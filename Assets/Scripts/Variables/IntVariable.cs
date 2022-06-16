using UnityEngine;

namespace Variables
{
  [CreateAssetMenu(fileName = "NewIntVariable", menuName = "Variables/Int")]
  public class IntVariable : ScriptableObject
  {
    [field: SerializeField] public int Value { get; set; }
  }
}