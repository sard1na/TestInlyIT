using UnityEngine;

public class InputManager : MonoBehaviour
{
    public JoystickController joystick;

    public Vector2 GetInput()
    {
        Vector2 keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (keyboardInput.magnitude > 0.1f)
        {
            return keyboardInput.normalized;
        }
        else
        {
            return joystick.InputDirection;
        }
    }
}
