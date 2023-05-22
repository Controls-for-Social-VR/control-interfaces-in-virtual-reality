using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    public Animator handAnimator;

    // Update is called once per frame
    void Update()
    {
        float triggerVal = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerVal);

        float gripVal = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripVal);

        if (!pinchAnimationAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(pinchAnimationAction);
            pinchAnimationAction = newProperty;
        }
        if (!gripAnimationAction.action.actionMap.enabled)
        {
            InputActionProperty newProperty = setRefererenceToActiveAction(gripAnimationAction);
            gripAnimationAction = newProperty;
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
                actionReference = new InputActionProperty(InputActionReference.Create(asset.actionMaps[i].FindAction(actionReference.action.name)));
                return actionReference;
            }
        }

        return actionReference;
    }
}
