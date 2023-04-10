using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ActionsList : MonoBehaviour
{
    /*
        Input Action objects

        controlScheme is the device mapping (Keyboard&Mouse, Gamepad, XR, Touch, ...). 
        Each control scheme could be made of one or more device, for example:
        Keyboard&Mouse(<Keyboard>, <Mouse>) OR Gamepad(<Gamepad>, <VirtualMouse>)

        actionMap.actions[i].RemoveAction(); To remove an action from an action map

        actionMap.actions[i].AddBinding(string path); To add a new binding. You need a 
        binding path for its creation

        actionMap.actions[i].ChangeBinding(int idx).Erase(); To delete the selected binding
    */

    TextMeshProUGUI textElement;
    string textString = "";

    [SerializeField]
    PlayerInput playerInput;

    // Switch between InputActions OR use PlayerInput and update the PlayerInput file with new configuration
    // new inputactions will be stored in a separate file

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

        // CONTINUE HERE: I was creating a new actionMap and wanted to add it to our 
        // actionmap asset, so that we can later swap action maps in the ui.\
        
        // InputActionMap actionMap = new InputActionMap();
        // actionMap = actionsContainer.asset.actionMaps[0];
        // for(int i = 0; i< actionMap.actions.Count; i++){
        //     actionMap.actions[i].ChangeBinding(int idx).Erase();
        // }
    }

    void OnEnable() {
        actionsContainer.Enable();
    }

    void OnDisable()
    {
        if (actionsContainer != null) {
            actionsContainer.Disable();
        }
        playerInput.onControlsChanged -= UpdateActionsList;
    }

    void UpdateActionsList(PlayerInput playerInput) {
        Debug.Log("BINDINGS LIST REFRESHED");

        for (int i = 0; i < actionsContainer.asset.actionMaps.Count; i++){
            Debug.Log(actionsContainer.asset.actionMaps[i]);
        }

        textString = "";
         
        string[] actionMapGroupSetArr = getActionMapGroupSet(actionsContainer);
        for(int j = 0; j < actionMapGroupSetArr.Length; j++) {
            textString += actionMapGroupSetArr[j];
            textString += "\n";
        }
        textString += "\n";

        for (int k = 0; k < actionsContainer.asset.actionMaps.Count; k++) {
            textString += actionsContainer.asset.actionMaps[k].name + " ACTIVE: " + actionsContainer.asset.actionMaps[k].enabled;
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

    string[] getActionMapGroupSet (PrototypeActions inputActionsObj) {
        HashSet<string> actionMapGroupSet = new HashSet<string>();
        
        for(int i = 0; i < inputActionsObj.asset.actionMaps.Count; i++) {
            string groupName = inputActionsObj.asset.actionMaps[i].name;
            actionMapGroupSet.Add(groupName.Substring(0, groupName.IndexOf('-')));
        }

        string[] actionMapGroupSetArr = new string[actionMapGroupSet.Count];
        actionMapGroupSet.CopyTo(actionMapGroupSetArr);

        return actionMapGroupSetArr;
    }
}
