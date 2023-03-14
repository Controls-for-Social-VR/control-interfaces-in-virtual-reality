using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class TriggeringDevice : MonoBehaviour
{
    public PlayerInput playerInput;
    string currentControlName = "";

    TextMeshProUGUI textElement;

    void Awake() {
        //playerInput = GetComponent<PlayerInput>();
        textElement = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput.onActionTriggered += LogCurrentlyUsedDevice;
    }

    private void OnDisable() {
        playerInput.onActionTriggered -= LogCurrentlyUsedDevice;
    }

    void LogCurrentlyUsedDevice(CallbackContext ctx)
    {
        // Update so that it contains XR inputs as well
        if (playerInput.currentControlScheme == "Gamepad")
        {
            currentControlName = Gamepad.current.name;
        }
        else if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            currentControlName = Keyboard.current.name;
        }
        textElement.text = currentControlName;
    }
}
