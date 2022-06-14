using UnityEngine;

namespace Variables 
{
  [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Variables/Float")]
  public class FloatVariable : ScriptableObject
  {
    [SerializeField] private float _value;
    public float Value { get => _value; set { _value = value; } }
  }
}