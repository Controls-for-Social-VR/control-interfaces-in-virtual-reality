using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Reflection;
using UnityEngine.XR;
using UnityEngine.EventSystems;

public class UpdateEventSystem : MonoBehaviour
{
    InputSystemUIInputModule eventManager;
    InputActionAsset actionsAsset;
    InputActionMap currentUIMap;
    
    

    // Start is called before the first frame update
    void Start()
    {
        eventManager = GetComponent<InputSystemUIInputModule>();
        actionsAsset = eventManager.actionsAsset;
        for (int i = 0; i < actionsAsset.actionMaps.Count; i++){
            if(actionsAsset.actionMaps[i].name.Contains("UI") && actionsAsset.actionMaps[i].enabled){
                currentUIMap = actionsAsset.actionMaps[i];
                break;
            }
        }
        Debug.Log("THE ACTIVE DEFAULT UI MAP IS " + currentUIMap.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentUIMap.enabled) {
            for (int i = 0; i < actionsAsset.actionMaps.Count; i++){
                if(actionsAsset.actionMaps[i].name.Contains("UI") && actionsAsset.actionMaps[i].enabled){
                    currentUIMap = actionsAsset.actionMaps[i];
                    for (int j = 0; j < currentUIMap.actions.Count; j++){
                        if (currentUIMap.actions[j].name.Contains("Point")) {
                            eventManager.point = InputActionReference.Create(currentUIMap.actions[j]);
                            //((InputSystemUIInputModule)EventSystem.current.currentInputModule).point = (currentUIMap.actions[j]);
                        } else if (currentUIMap.actions[j].name.Contains("Click")) {
                            eventManager.leftClick= InputActionReference.Create(currentUIMap.actions[j]);
                        } else if (currentUIMap.actions[j].name.Contains("MiddleClick")) {
                            eventManager.middleClick= InputActionReference.Create(currentUIMap.actions[j]);
                        } else if (currentUIMap.actions[j].name.Contains("RightClick")) {
                            eventManager.rightClick= InputActionReference.Create(currentUIMap.actions[j]);
                        } else if (currentUIMap.actions[j].name.Contains("ScrollWheel")) {
                            eventManager.scrollWheel= InputActionReference.Create(currentUIMap.actions[j]);
                        } else if (currentUIMap.actions[j].name.Contains("Navigate")) {
                            eventManager.move= InputActionReference.Create(currentUIMap.actions[j]);
                        } else if (currentUIMap.actions[j].name.Contains("Submit")) {
                            eventManager.submit= InputActionReference.Create(currentUIMap.actions[j]);
                        } else if (currentUIMap.actions[j].name.Contains("Cancel")) {
                            eventManager.cancel= InputActionReference.Create(currentUIMap.actions[j]);
                        } else if (currentUIMap.actions[j].name.Contains("TrackedDevicePosition")) {
                            eventManager.trackedDevicePosition= InputActionReference.Create(currentUIMap.actions[j]);
                        } else {
                            eventManager.trackedDeviceOrientation= InputActionReference.Create(currentUIMap.actions[j]);
                        } 
                    }
                    eventManager.ActivateModule();
                    break;
                }
            }
        }
    }
}
