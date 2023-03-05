using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckConnectedDevices : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        InputSystem.Update();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Debug.Log("This is the list of currently connected devices");
        // Debug.Log("Number of devices: " + InputSystem.devices.Count);
        // for (int i = 0; i < InputSystem.devices.Count; i++)
        // {
        //     Debug.Log(InputSystem.devices[i].name);
        // } 
    }
}
