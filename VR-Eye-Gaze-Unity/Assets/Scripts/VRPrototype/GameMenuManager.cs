using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;

    public Transform head;
    public float spawnDistance = 2;

    // Update is called once per frame
    void Update()
    {
        if (!showButton.action.actionMap.enabled)
        {
            Debug.Log("Changing Menu Button!!!");
            InputActionProperty newProperty = setRefererenceToActiveAction(showButton);
            showButton = newProperty;
        }

        if (showButton.action.WasPressedThisFrame()){
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }

    InputActionProperty setRefererenceToActiveAction(InputActionProperty actionReference)
    {
        string actionName = actionReference.action.name;
        string actionMapName = actionReference.action.actionMap.name;
        //Debug.Log("The action name is " + actionName);
        //Debug.Log("The action map name is " + actionMapName);

        // List of all actionMaps of the InputAction asset
        //actionReference.action.actionMap.asset.actionMaps

        // Get action group name (Lefthand Locomotion)
        string actionGroup = actionMapName.Substring(0, actionMapName.IndexOf('-'));
        //Debug.Log("The action map group name is " + actionGroup);
        // set actionReference to the first active action of that group
        InputActionAsset asset = actionReference.action.actionMap.asset;
        for (int i = 0; i < asset.actionMaps.Count; i++)
        {
            if (!asset.actionMaps[i].name.Contains(actionGroup)) continue;

            if (asset.actionMaps[i].enabled)
            {
                actionReference = new InputActionProperty(InputActionReference.Create(asset.actionMaps[i].FindAction(actionReference.action.name)));
                return actionReference;
            }
        }

        return actionReference;
    }
}
