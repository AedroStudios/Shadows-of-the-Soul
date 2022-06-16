using System;
using UnityEngine;

namespace Variables
{
  [Serializable]
  public class IntReference
  {
    [SerializeField] private int _constantValue;
    [SerializeField] private bool _useConstant;
    [SerializeField] private IntVariable _variable;

    public float Value => _useConstant ? _constantValue : _variable.Value;
  }
}