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

    // Object instance of Input Action Asset: CHANGE IT TO THE NAME OF FINAL IAA. There should be an IAA for each player.
    PrototypeActions actionsContainer;

    void Awake() {
        actionsContainer = new PrototypeActions();
        textElement = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateActionsList(playerInput);
        playerInput.onControlsChanged += UpdateActionsList;
        
    }

    void OnEnable() {
        actionsContainer.Enable();
    }

    void OnDisable()
    {
        actionsContainer.Disable();
        playerInput.onControlsChanged -= UpdateActionsList;
    }

    void UpdateActionsList(PlayerInput playerInput) {
        Debug.Log("BINDINGS LIST REFRESHED");

        for (int i = 0; i < actionsContainer.asset.actionMaps.Count; i++){
            Debug.Log(actionsContainer.asset.actionMaps[i]);
        }
        textString = "";
        for (int k = 0; k < actionsContainer.asset.actionMaps.Count; k++) {
            textString += actionsContainer.asset.actionMaps[k].name;
            textString += "\n";
            InputAction[] actions = actionsContainer.asset.actionMaps[k].actions.ToArray();
            for (int i = 0; i < actions.Length; i++)
            {
                textString += "    "+actions[i].name;
                textString += "\n";
                InputBinding[] bindings = actions[i].bindings.ToArray();
            
                // for (int j = 0; j < bindings.Length; j++){
                //     textString += "    "+bindings[j].path;
                //     textString += "\n";
                // }
            } 
        }
        textElement.text = textString;
    }
}
