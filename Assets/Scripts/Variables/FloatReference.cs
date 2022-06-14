using System;
using UnityEngine;

namespace Variables 
{
  [Serializable]
  public class FloatReference
  {
    [SerializeField] private float _constantValue;
    [SerializeField] private bool _useConstant;
    [SerializeField] private FloatVariable _variable;

    public float Value => _useConstant ? _constantValue : _variable.Value;
  }
}