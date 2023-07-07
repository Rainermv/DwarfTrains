using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.DwarfTrain.Scripts
{
    public class PlayerInputController
    {
        private readonly PlayerInput _playerInput;

        public PlayerInputController(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.onActionTriggered += PlayerInputActionTriggered;         }

        private void PlayerInputActionTriggered(InputAction.CallbackContext inputActionCallbackContext)
        {
            //inputActionCallbackContext.

            switch (inputActionCallbackContext.action.name)
            {
                // Move action pressed
                case InputActions.Move when inputActionCallbackContext.action.IsPressed():
                    PlayerInputEvents.OnMove(inputActionCallbackContext.action.ReadValue<Vector2>());
                    break;

                // Move action released
                case InputActions.Move:
                    PlayerInputEvents.OnMove(Vector2.zero);
                    break;

                // Zoom action
                case InputActions.Zoom:
                    var value = inputActionCallbackContext.action.ReadValue<float>();
                    if (value > 0) PlayerInputEvents.OnZoom(1);
                    if (value < 0) PlayerInputEvents.OnZoom(-1);
                    if (value == 0) PlayerInputEvents.OnZoom(0);
                    break;


                default:
                    return;

            }
        }
    }
}