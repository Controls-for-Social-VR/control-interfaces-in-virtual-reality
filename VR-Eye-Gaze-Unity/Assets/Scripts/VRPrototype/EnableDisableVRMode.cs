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
    // Update is called once per frame
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
            //camera.transform.rotation = new Quaternion(0, camera.transform.rotation.y, camera.transform.rotation.z, camera.transform.rotation.w);
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
