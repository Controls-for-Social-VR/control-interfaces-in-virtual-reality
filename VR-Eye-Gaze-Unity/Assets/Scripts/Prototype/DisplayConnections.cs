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
    }

    // Update is called once per frame
    void Update()
    {
        textString = "";
        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            textString += InputSystem.devices[i].name;
            textString += "\n";
        } 
        textElement.text = textString;
    }

    
}


