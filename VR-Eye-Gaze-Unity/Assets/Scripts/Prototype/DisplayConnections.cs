using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DisplayConnections : MonoBehaviour
{
    TextMeshProUGUI textElement;
    string textString = "";
    // Start is called before the first frame update
    void Start()
    {
        textElement = GetComponent<TextMeshProUGUI>();
        UpdateDeviceList();
        InputSystem.onDeviceChange += (InputDevice device, InputDeviceChange change) => {
            UpdateDeviceList();
        };
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= (InputDevice device, InputDeviceChange change) => {
            UpdateDeviceList();
        };
    }

    void UpdateDeviceList() {
        textString = "";
        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            if (InputSystem.devices[i].name.Contains("VirtualMouse")) {
                continue;
            }
            textString += InputSystem.devices[i].name;
            textString += "\n";
        } 
        textElement.text = textString;
    }

    
}


