using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputsActions;

namespace Platformer
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Platformer/InputReader")]
    public class InputReader : ScriptableObject, IPlatformerActions, IDungeonActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2> Aim = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction EnableMouseControlCamera = delegate { };
        public event UnityAction DisableMouseControlCamera = delegate { };

        PlayerInputsActions inputActions;

        public Vector3 Direction => inputActions.Platformer.Move.ReadValue<Vector2>();

        void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputsActions();
                inputActions.Platformer.SetCallbacks(this);
                inputActions.Dungeon.SetCallbacks(this);
            }
            
        }

        public void EnablePlayerActions()
        {
            inputActions.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }
        public void OnAim(InputAction.CallbackContext context)
        {
            Aim.Invoke(context.ReadValue<Vector2>());
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            //noop
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            //noop
        }


        public void OnRun(InputAction.CallbackContext context)
        {
            //noop
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            //noop
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Look.Invoke(context.ReadValue<Vector2>(), isDeviceMouse(context));
        }

        bool isDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

        public void OnMouseControlCamera(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                EnableMouseControlCamera.Invoke();
                break;
                case InputActionPhase.Canceled:
                DisableMouseControlCamera.Invoke();
                break;
            }
        }
    }
}
