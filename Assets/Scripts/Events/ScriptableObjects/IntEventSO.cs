using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects {
	[CreateAssetMenu(menuName = "Events/Int Event Channel")]
    public class IntEventSO : ScriptableObject, IGameEvent<int> {

    public UnityAction<int> OnEventRaised { get; set; }
  }
}