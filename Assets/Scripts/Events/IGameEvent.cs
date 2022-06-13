using UnityEngine.Events;

namespace Events
{
  /// <summary>
  /// Crear un evento sin variables
  /// </summary>
  public interface IGameEvent
  {
    public UnityAction OnEventRaised { get; set; }

    public void RaiseEvent()
    {
      OnEventRaised?.Invoke();
    }
  }

  /// <summary>
  /// Crear un evento con una variable
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IGameEvent<T>
  {
    public UnityAction<T> OnEventRaised { get; set; }

    public void RaiseEvent(T t)
    {
      OnEventRaised?.Invoke(t);
    }
  }

  /// <summary>
  /// Crear un Evento de Dos variables
  /// </summary>
  /// <typeparam name="T1"></typeparam>
  /// <typeparam name="T2"></typeparam>
  public interface IGameEvent<T1, T2>
  {
    public UnityAction<T1, T2> OnEventRaised { get; set; }

    public void RaiseEvent(T1 t, T2 v)
    {
      OnEventRaised?.Invoke(t, v);
    }
  }

  /// <summary>
  /// Crear un evento de tres variables
  /// </summary>
  /// <typeparam name="T1"></typeparam>
  /// <typeparam name="T2"></typeparam>
  /// <typeparam name="T3"></typeparam>
  public interface IGameEvent<T1, T2, T3>
  {
    public UnityAction<T1, T2, T3> OnEventRaised { get; set; }

    public void RaiseEvent(T1 t, T2 v, T3 x)
    {
      OnEventRaised?.Invoke(t, v, x);
    }
  }
}