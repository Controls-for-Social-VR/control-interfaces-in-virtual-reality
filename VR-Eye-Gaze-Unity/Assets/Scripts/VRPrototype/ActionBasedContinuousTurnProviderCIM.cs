using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActionBasedContinuousTurnProviderCIM : MonoBehaviour
{
    public ActionBasedContinuousTurnProvider provider;

    void Update()
    {
         if (!provider.rightHandTurnAction.action.actionMap.enabled)
        {
            Debug.Log("Inside IF statement");
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.rightHandTurnAction);
            provider.rightHandTurnAction = newProperty;
            Debug.Log(provider.rightHandTurnAction.action.name);
        }
    }

    InputActionProperty setRefererenceToActiveAction(InputActionProperty actionReference)
    {
        string actionName = actionReference.action.name;
        string actionMapName = actionReference.action.actionMap.name;
        string actionGroup = actionMapName.Substring(0, actionMapName.IndexOf('-'));
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
