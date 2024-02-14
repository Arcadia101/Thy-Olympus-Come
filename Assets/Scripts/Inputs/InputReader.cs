using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Platformer
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Platformer/InputReader")]
    public class InputReader : ScriptableObject, PlayerInputsActions.IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2> Aim = delegate { };

        PlayerInputsActions inputActions;

        public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();

        void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputsActions();
                inputActions.Player.SetCallbacks(this);
            }
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
    }
}
