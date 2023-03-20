using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ActionsList : MonoBehaviour
{
    TextMeshProUGUI textElement;
    string textString = "";

    [SerializeField]
    PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        textElement = GetComponent<TextMeshProUGUI>();
        UpdateActionsList(playerInput);
        playerInput.onControlsChanged += UpdateActionsList;
        
    }

    void OnDisable()
    {
        playerInput.onControlsChanged -= UpdateActionsList;
    }

    void UpdateActionsList(PlayerInput playerInput) {
        textString = "";
        InputAction[] actions = playerInput.currentActionMap.actions.ToArray();
        for (int i = 0; i < actions.Length; i++)
        {
            textString += actions[i].name;
            textString += "\n";
            InputBinding[] bindings = actions[i].bindings.ToArray();
            
            for (int j = 0; j < bindings.Length; j++){
                Debug.Log("BINDING: " + bindings[j].path);
                textString += "  "+bindings[j].path;
                textString += "\n";
            }
        } 
        textElement.text = textString;
    }
}
