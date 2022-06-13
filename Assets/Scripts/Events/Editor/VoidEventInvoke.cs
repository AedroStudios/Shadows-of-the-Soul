using Events.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Events.Editor
{
  [CustomEditor(typeof(VoidEventSO))]
  public class VoidEventInvoke : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();
      if (!GUILayout.Button("Invoke Event"))
      {
        return;
      }
      var voidEvent = target as IGameEvent;
      voidEvent?.RaiseEvent();
    }
  }
}