using UnityEngine;
using Variables;

namespace Player 
{
  
  public class PlayerFade : MonoBehaviour 
  {
    #region VALUES
    private Material _playerFadeMat;
    [SerializeField] private FloatReference _maxStamina, _actualStamina;
    #endregion

    private void Awake()
    {
      _playerFadeMat = GetComponent<SpriteRenderer>().material;
      _playerFadeMat.SetFloat("_MaxStamina", _maxStamina.Value);
    }

    void Update()
    {
      _playerFadeMat.SetFloat("_ActualStamina", _actualStamina.Value);
    }

  }
}