using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionCIM : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject leftRay;
    public GameObject rightHand;
    public GameObject rightRay;

    public GameObject unmovingHand;
    public GameObject unmovingRay;

    public InputActionProperty interactionMode;

    // Update is called once per frame
    void Update()
    {
        if (!interactionMode.action.actionMap.enabled)
        {
            interactionMode = setRefererenceToActiveAction(interactionMode);
        }

        if (interactionMode.action.actionMap.name.Contains("Gamepad") || interactionMode.action.actionMap.name.Contains("Mouse") || interactionMode.action.actionMap.name.Contains("Keyboard"))
        {
            // Disable left hand
            leftHand.SetActive(false);
            leftRay.SetActive(false);
            rightHand.SetActive(false);
            rightRay.SetActive(false);

            unmovingHand.SetActive(true);
            unmovingRay.SetActive(true);
        }
        else
        {
            // Enable left hand
            leftHand.SetActive(true);
            leftRay.SetActive(true);
            rightHand.SetActive(true);
            rightRay.SetActive(true);

            unmovingHand.SetActive(false);
            unmovingRay.SetActive(false);
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
