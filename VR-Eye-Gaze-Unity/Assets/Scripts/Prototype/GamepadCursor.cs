using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    private Mouse virtualMouse;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private RectTransform cursorTransform;

    [SerializeField]
    private float speed = 1000;

    [SerializeField]
    private RectTransform canvasRectTransform;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Canvas canvas;

    private bool prevMouseState;

    void Start() {
        InputSystem.onDeviceChange += TriggerCursor;
    }

    void TriggerCursor(InputDevice device, InputDeviceChange change) {
        InputDevice[] devicesArr = InputSystem.devices.ToArray();

        // if (Gamepad.current != null && Gamepad.current.enabled) {
        //         Debug.Log("THERE IS A CONTROLLER");

        //         cursorTransform.gameObject.SetActive(true);
        //         Vector2 initialState = new Vector2(0, 0);
        //         AnchorCursor(initialState);
        //         InputState.Change(virtualMouse.position, initialState);
        //     } else {
        //         Debug.Log("There isn't a controller!!!!!!");
        //         cursorTransform.gameObject.SetActive(false);
        //     }

        if(InputSystem.GetDevice("VirtualMouse").enabled){
            Debug.Log("THERE IS A CONTROLLER");

            cursorTransform.gameObject.SetActive(true);
            Vector2 initialState = new Vector2(0, 0);
            AnchorCursor(initialState);
            InputState.Change(virtualMouse.position, initialState);
        } else {
            Debug.Log("There isn't a controller!!!!!!");
            cursorTransform.gameObject.SetActive(false);
        }
    }
    
    private void OnEnable() {

        InputDevice[] devicesArr = InputSystem.devices.ToArray();
        for (int i = 0; i < devicesArr.Length; i++){
            if(devicesArr[i].name.Contains("VirtualMouse")) {
                InputSystem.RemoveDevice(devicesArr[i]);
            }
        }
        if (virtualMouse == null) {
            virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added) {
            InputSystem.AddDevice(virtualMouse);
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null) {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable () {
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion() {
        if (virtualMouse == null || Gamepad.current == null) {
            return;
        }

        Vector2 stickValue = Gamepad.current.leftStick.ReadValue();
        stickValue *= speed * Time.deltaTime;

        Vector2 currentPos = virtualMouse.position.ReadValue();
        Vector2 newPos = currentPos + stickValue;

        newPos.x = Mathf.Clamp(newPos.x, 0, Screen.width);
        newPos.y = Mathf.Clamp(newPos.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPos);
        InputState.Change(virtualMouse.delta, stickValue);

        bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if(prevMouseState != aButtonIsPressed){
            virtualMouse.CopyState<MouseState>(out var mouseState);

            mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            prevMouseState = aButtonIsPressed;
        }

        AnchorCursor(newPos);
    }
    
    private void AnchorCursor(Vector2 position) {
        Vector2 anchoredPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);

        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
