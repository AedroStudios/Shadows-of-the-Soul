using UnityEngine;

namespace Variables 
{
  [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Variables/Float")]
  public class FloatVariable : ScriptableObject
  {
    [field: SerializeField] public float Value { get; set; }
  }
}