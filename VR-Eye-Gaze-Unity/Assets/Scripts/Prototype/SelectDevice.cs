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

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        currentDevice = "Keyboard&Mouse";
        previousDevice = currentDevice;
        UpdateDropdown();
        InputSystem.onDeviceChange += (InputDevice device, InputDeviceChange change) =>
        {
            UpdateDropdown();
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(previousDevice != currentDevice) {
            InputDevice[] devicesArr = InputSystem.devices.ToArray();
        for(int i = 0; i < devicesArr.Length; i++) {
            Debug.Log(devicesArr[i].name);
            InputSystem.EnableDevice(devicesArr[i]);
        }

        for(int i = 0; i < devicesArr.Length; i++) {
            // Remove all devices that are not currentDevice (check with index)
            // Make sure you do not disable mouse or keyboard cause they are two separate devices
            if(!currentDevice.Contains(devicesArr[i].name)) {
                InputSystem.DisableDevice(devicesArr[i]);
            }
        }

            previousDevice = currentDevice;
        }
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
        deviceNames = new string[InputSystem.devices.Count - 1];
        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            if (InputSystem.devices[i].name == "Keyboard")
            {
                deviceNames[i] = "Keyboard&Mouse";
                i++;
            }
            else
            {
                deviceNames[i - 1] = InputSystem.devices[i].name;
            }
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
        
        
        Debug.Log("Current device is: " + currentDevice);
    }
}
