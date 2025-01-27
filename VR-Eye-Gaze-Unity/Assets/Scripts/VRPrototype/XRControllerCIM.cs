using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRControllerCIM : MonoBehaviour
{
    public ActionBasedController provider;

    void Update()
    {
 
        if (!provider.positionAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.positionAction);
            provider.positionAction = newProperty;
        }  
        if (!provider.rotationAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.rotationAction);
            provider.rotationAction = newProperty;
        }  
        if (!provider.trackingStateAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.trackingStateAction);
            provider.trackingStateAction = newProperty;
        }  
        if (!provider.selectAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.selectAction);
            provider.selectAction = newProperty;
        }  
        if (!provider.selectActionValue.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.selectActionValue);
            provider.selectActionValue = newProperty;
        }
        if (!provider.activateAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.activateAction);
            provider.activateAction = newProperty;
        }
        if (!provider.activateActionValue.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.activateActionValue);
            provider.activateActionValue = newProperty;
        }
        if (!provider.uiPressAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.uiPressAction);
            provider.uiPressAction = newProperty;
        }
        if (!provider.uiPressActionValue.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.uiPressActionValue);
            provider.uiPressActionValue = newProperty;
        }
        if (!provider.hapticDeviceAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.hapticDeviceAction);
            provider.hapticDeviceAction = newProperty;
        }
        if (!provider.rotateAnchorAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.rotateAnchorAction);
            provider.rotateAnchorAction = newProperty;
        }
        if (!provider.translateAnchorAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(provider.translateAnchorAction);
            provider.translateAnchorAction = newProperty;
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
                actionReference = new InputActionProperty(InputActionReference.Create(asset.actionMaps[i].FindAction(actionReference.action.name)));
                return actionReference;
            }
        }

        return actionReference;
    }
}
