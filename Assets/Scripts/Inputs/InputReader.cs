using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

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
        public event UnityAction Attack = delegate { };
        public event UnityAction Skill = delegate { };
        public event UnityAction<bool> Run = delegate { };
        public event UnityAction<bool> Jump = delegate { };
        public event UnityAction<bool> Dash = delegate { };
        public event UnityAction<bool> SelectingSkill = delegate { };

        PlayerInputActions inputActions;

        public Vector3 Direction => inputActions.Platformer.Move.ReadValue<Vector2>();

        void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputActions();
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
            if (context.phase == InputActionPhase.Started)
            {
                Attack.Invoke();
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                Jump.Invoke(true);
                break;
                case InputActionPhase.Canceled:
                Jump.Invoke(false);
                break;
            }
        }


        public void OnRun(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Run.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Run.Invoke(false);
                    break;
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Dash.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Dash.Invoke(false);
                    break;
            }
        }

        public void OnSkill(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Skill.Invoke();
            }
        }

        public void OnSelectSkill(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    SelectingSkill.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    SelectingSkill.Invoke(false);
                    break;
            }
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
