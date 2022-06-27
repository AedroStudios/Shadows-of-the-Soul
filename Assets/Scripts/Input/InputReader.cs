using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
  [CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader", order = 51)]
  public class InputReader : ScriptableObject, GameInput.IGamePlayActions
  {
    private GameInput gameInput;

    private void OnEnable()
    {
      if (gameInput == null)
      {
        gameInput = new GameInput();
        gameInput.GamePlay.SetCallbacks(this);
      }

      gameInput.GamePlay.Enable(); // TODO: Activar desde el game manager en el futuro
    }

    private void OnDisable()
    {
      gameInput.Disable();
    }

    #region GamePlayInputs

    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCanceledEvent = delegate { };
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction StealthEvent = delegate { };
    public event UnityAction LetGoIfCaughtEvent = delegate { };
    public event UnityAction DropEnergyPressed = delegate { };
    public event UnityAction DropEnergyReleased = delegate { };


    public void OnJump(InputAction.CallbackContext context)
    {
      switch (context.phase)
      {
        case InputActionPhase.Started:
          JumpEvent.Invoke();
          break;
        case InputActionPhase.Canceled:
          JumpCanceledEvent.Invoke();
          break;
      }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
      switch (context.phase)
      {
        case InputActionPhase.Performed:
        case InputActionPhase.Canceled:
          MoveEvent.Invoke(context.ReadValue<Vector2>());
          break;
      }
    }

    public void OnStealth(InputAction.CallbackContext context)
    {
      if (InputActionPhase.Started == context.phase)
      {
        StealthEvent.Invoke();
      }
    }

    public void OnLetGoWhenIsCaught(InputAction.CallbackContext context)
    {
      if (InputActionPhase.Started == context.phase)
      {
        LetGoIfCaughtEvent.Invoke();
      }
    }

    public void OnDropEnergy(InputAction.CallbackContext context)
    {
      switch (context.phase)
      {
       case InputActionPhase.Started:
         DropEnergyPressed.Invoke();
         break;
       case InputActionPhase.Canceled:
         DropEnergyReleased.Invoke();
         break;
       case InputActionPhase.Disabled:
       case InputActionPhase.Waiting:
       case InputActionPhase.Performed:
       default:
         break;
      }
    }

    #endregion

  }
}