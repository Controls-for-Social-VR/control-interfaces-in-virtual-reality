using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SelectDevice : MonoBehaviour, IPointerClickHandler
{
    TMP_Dropdown dropdown;
    string currentDevice;
    string previousDevice;
    string[] deviceNames;

    void Awake() {
        dropdown = GetComponent<TMP_Dropdown>();
        currentDevice = "Keyboard&Mouse";
        previousDevice = currentDevice;
        UpdateSelection();
        UpdateDropdown();
    }

    // Start is called before the first frame update
    void Start()
    {
        InputSystem.onDeviceChange += (InputDevice device, InputDeviceChange change) =>
        {
            UpdateDropdown();
        };
        
    }

    // Update is called once per frame
    void Update()
    {
        if(previousDevice != currentDevice) {
            UpdateSelection();
        }
    }

    void UpdateSelection() {
        InputDevice[] devicesArr = InputSystem.devices.ToArray();
        for(int i = 0; i < devicesArr.Length; i++) {
            InputSystem.EnableDevice(devicesArr[i]);
        }

        for(int i = 0; i < devicesArr.Length; i++) {
            // Remove all devices that are not currentDevice (check with index)
            // Make sure you do not disable mouse or keyboard cause they are two separate devices
            if(!currentDevice.Contains(devicesArr[i].name)) {
                InputSystem.DisableDevice(devicesArr[i]);
            }
        }

        if(InputSystem.GetDevice(currentDevice) is Gamepad){
            InputSystem.EnableDevice(InputSystem.GetDevice("VirtualMouse"));
            Debug.Log("ENABLED VIRTUALMOUSE! " + InputSystem.GetDevice("VirtualMouse").enabled);
        } else {
            Debug.Log("Current Gamepad " + Gamepad.current.name);
        }

        previousDevice = currentDevice;
    }

    private void OnDisable () {
        InputSystem.onDeviceChange -= (InputDevice device, InputDeviceChange change) =>
        {
            UpdateDropdown();
        };
    }

    public void OnPointerClick(PointerEventData ped)
    {
        if (!dropdown.Equals(null)) {
            UpdateDropdown();
        }
    }

    void PopulateDropdown(TMP_Dropdown dropdown, string[] optionsArray)
    {
        List<string> options = new List<string>();
        foreach (var option in optionsArray)
        {
            options.Add(option);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    void UpdateDropdown()
    {
        if (dropdown == null) {
            return;
        }
        deviceNames = new string[InputSystem.devices.Count - 2];
        try {
            for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            if (InputSystem.devices[i].name == "Keyboard")
            {
                deviceNames[i] = "Keyboard&Mouse";
                i++;
            } else if (InputSystem.devices[i].name.Contains("VirtualMouse")) {
                i++;
                continue;
            }
            else
            {
                deviceNames[i - 1] = InputSystem.devices[i].name;
            }
        }
        } catch {
            Debug.Log("Check that we have a Keyboard, a Mouse, and a VirtualMouse");
        }
        

        PopulateDropdown(dropdown, deviceNames);
        dropdown.GetComponentInChildren<TMP_Text>().SetText(currentDevice);

        int idx = -1;
        for (int i = 0; i < deviceNames.Length; i++)
        {
            if (currentDevice == deviceNames[i])
            {
                idx = i;
            }
        }
        if (idx == -1)
        {
            idx = 0;
            currentDevice = deviceNames[idx];
            dropdown.GetComponentInChildren<TMP_Text>().SetText(currentDevice);
        }
        dropdown.value = idx;
    }

    public void DeviceSelection(int index)
    {
        currentDevice = deviceNames[index];
        
        
        //Debug.Log("Current device is: " + currentDevice);
    }
}
