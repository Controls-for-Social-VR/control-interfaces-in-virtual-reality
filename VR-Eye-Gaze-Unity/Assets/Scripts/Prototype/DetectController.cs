using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class DetectController : MonoBehaviour
{
    public PlayerInput playerInput;

    TextMeshProUGUI textElement;
    string textString = "";

    void Awake() {
        //playerInput = GetComponent<PlayerInput>();
        textElement = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput.onActionTriggered += (CallbackContext ctx) =>
        {
            textElement.text = ctx.action.name;
        };
    }

    private void OnDisable() {
        playerInput.onActionTriggered -= (CallbackContext ctx) =>
        {
            textElement.text = ctx.action.name;
        };
    }
}
