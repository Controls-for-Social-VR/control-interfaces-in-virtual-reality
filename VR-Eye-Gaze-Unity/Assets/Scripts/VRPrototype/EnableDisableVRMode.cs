using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Management;

public class EnableDisableVRMode : MonoBehaviour
{
    public InputActionProperty VRMode;
    public Transform cameraOffset;
    public Camera camera;
    bool initialised = false;
    bool needsReset = true;

    void Update()
    {
        if (!VRMode.action.actionMap.enabled)
        {
            VRMode = setRefererenceToActiveAction(VRMode);
        }

        if (VRMode.action.actionMap.name.Contains("Disabled") && needsReset)
        {
            StopXR();
            cameraOffset.position = new Vector3(cameraOffset.position.x, 1.4f, cameraOffset.position.z);
            camera.fieldOfView = 60f;
            transform.rotation = new Quaternion(0, 0, 0, 0);

            initialised = false;
            needsReset = false;
        } else if (!VRMode.action.actionMap.name.Contains("Disabled") && !initialised)
        {
            initialised = true;
            cameraOffset.position = new Vector3(cameraOffset.position.x, 0f, cameraOffset.position.z);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            StopXR();
            StartCoroutine(StartXRCoroutine());
            transform.rotation = new Quaternion(0, 0, 0, 0);
            needsReset = true;
        }
    }

    public IEnumerator StartXRCoroutine()
    {
        Debug.Log("Initializing XR...");
        if (!XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

            if (XRGeneralSettings.Instance.Manager.activeLoader == null)
            {
                Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
            }
            else
            {
                Debug.Log("Starting XR...");
                XRGeneralSettings.Instance.Manager.StartSubsystems();
            }
        }
    }

    void StopXR()
    {
        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
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
