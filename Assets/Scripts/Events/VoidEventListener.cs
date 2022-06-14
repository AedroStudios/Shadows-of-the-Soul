using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
  public class VoidEventListener : MonoBehaviour
  {
    [SerializeField, Tooltip("Esto tiene que estar asignado")] VoidEventSO channel;
    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
      channel.OnEventRaised += Response;
    }

    private void OnDisable()
    {
      channel.OnEventRaised -= Response;
    }

    private void Response()
    {
      response?.Invoke();
    }
  }
}
