using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActionBasedContinuousMoveProviderCIM : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider provider;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("The Move Provider's leftHandAction relative actionMap is enabled: " + leftHandMoveAction.action.actionMap.enabled);
        if (!provider.leftHandMoveAction.action.actionMap.enabled)
        {
            Debug.Log("Inside IF statement");
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.leftHandMoveAction);
            provider.leftHandMoveAction= newProperty;
            Debug.Log(provider.leftHandMoveAction.action.name);
        }
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
                Debug.Log("THIS IS ACTIVE" + asset.actionMaps[i].name);
                actionReference = new InputActionProperty(InputActionReference.Create(asset.actionMaps[i].FindAction(actionReference.action.name)));
                return actionReference;
            }
        }

        return actionReference;
    }

}
