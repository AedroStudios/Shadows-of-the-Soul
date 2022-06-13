using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects {
	[CreateAssetMenu(menuName = "Events/Create New VoidEventSO")]
    public class VoidEventSO : ScriptableObject, IGameEvent
  {
    public UnityAction OnEventRaised { get; set; }
  }
}