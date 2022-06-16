using UnityEngine;
using Variables;

namespace Energy 
{
  public class BodyFade : MonoBehaviour 
  {
    #region VALUES
    private Material _bodyFadeMat;
    [SerializeField] private IntReference _maxEnergy, _actualEnergy;
    #endregion

    private void Awake()
    {
      _bodyFadeMat = GetComponent<SpriteRenderer>().material;
      _bodyFadeMat.SetFloat("_MaxStamina", (float)_maxEnergy.Value);
    }
    void Update()
    {
      _bodyFadeMat.SetFloat("_ActualStamina", (float)_actualEnergy.Value);
    }
  }
}