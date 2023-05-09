using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Reflection;
using UnityEngine.XR;

public class ActionItem : MonoBehaviour
{
    string actionGroupName;
    string[] configurationNames;
    
    bool isOpen = false;
    float currentHeight;

    [SerializeField]
    float expandedHeight;

    RectTransform itemTransform;

    [SerializeField]
    RectTransform buttonTransform;

    TextMeshProUGUI textElement;

    [SerializeField]
    GameObject extras;

    InputActionAsset actionsAsset;

    [SerializeField]
    GameObject configPrefab;

    [SerializeField]
    Transform configContainer;
    
    [SerializeField]
    Transform configDetailsContainer;

    [SerializeField]
    GameObject configDetailsItemPrefab;

    string[] currentConfigurationDevices;

    [SerializeField]
    GameObject bindingInfoPrefab;

    [SerializeField]
    Transform bindingInfoContainer;

    [SerializeField]
    RectTransform setupContainer;

    [SerializeField]
    RectTransform configurationSelectors;

    // Start is called before the first frame update
    void Start()
    {
        itemTransform = GetComponent<RectTransform>();
        currentHeight = itemTransform.rect.height;
        textElement = buttonTransform.GetComponentInChildren<TextMeshProUGUI>();
        extras.SetActive(false);
    }

    void Update() {
        resizeExtrasWindow();
    }

    void resizeExtrasWindow() {
        if (isOpen){
            LayoutRebuilder.MarkLayoutForRebuild((RectTransform)configDetailsContainer);
            LayoutRebuilder.MarkLayoutForRebuild(setupContainer);
            LayoutRebuilder.MarkLayoutForRebuild(configurationSelectors);  
            float expandedHeightTemp = configDetailsContainer.GetComponent<RectTransform>().rect.height + setupContainer.rect.height + configurationSelectors.rect.height;
            if (expandedHeightTemp < 100) {
                expandedHeight = 200;
            } else if (expandedHeightTemp < 200) {
                expandedHeight = 300;
            } else if (expandedHeightTemp < 300) {
                expandedHeight = 400;
            } else if (expandedHeightTemp < 350) {
                expandedHeight = 450;
            } else if (expandedHeightTemp < 450) {
                expandedHeight = 550;
            }
            itemTransform.sizeDelta = new Vector2(itemTransform.rect.width, expandedHeight);
        }
    }

    public void FeedActionItem (string actionGroupName, InputActionAsset actionsAsset) {
        this.actionGroupName = actionGroupName;
        this.actionsAsset = actionsAsset;

        TextMeshProUGUI textElement = GetComponentInChildren<TextMeshProUGUI>();
        textElement.text = actionGroupName;

        // Create list of Configuration Names
        configurationNames = getConfigurationNames(actionsAsset, actionGroupName);
        // Instantiate One ConfigurationItem per Configuration Name. A Configuration is an Action Group with a Config name next to it (Player-Default)
        for(int i = 0; i < configurationNames.Length; i++){
            GameObject newItem = Instantiate(configPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0 ,0));
            string configName = configurationNames[i];
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = configName;
            newItem.GetComponent<Button>().onClick.AddListener(delegate{SwitchConfig(actionGroupName, configName);});
            newItem.transform.SetParent(configContainer, false);
            ReEnableAfterFrame(configContainer.gameObject);
        }
        

        // Set action to Default configuration
        SwitchConfig(actionGroupName, "Default");

        Canvas.ForceUpdateCanvases();
    } 

    public void ExpandRetractItem () {
        if (isOpen) {
            itemTransform.sizeDelta = new Vector2(itemTransform.rect.width, currentHeight);
            textElement.text = "Open";
            extras.SetActive(false);
        } else {
            textElement.text = "Close";
            extras.SetActive(true);
            resizeExtrasWindow();
        }

        isOpen = !isOpen;
    }

    string[] getConfigurationNames (InputActionAsset inputActionsObj, string actionGroupName) {
        HashSet<string> configSet = new HashSet<string>();
        
        for(int i = 0; i < inputActionsObj.actionMaps.Count; i++) {
            string fullName = inputActionsObj.actionMaps[i].name;
            if (!fullName.Substring(0, fullName.IndexOf('-')).Equals(actionGroupName)) {
                continue;
            }
            configSet.Add(fullName.Substring(fullName.IndexOf('-')+1));
        }

        string[] configSetArr = new string[configSet.Count];
        configSet.CopyTo(configSetArr);

        return configSetArr;
    }

    void SwitchConfig (string actionGroup, string configName) {
        Debug.Log("SWITCHING CONFIGURATION FOR " + actionGroup + " TO " + configName);
        if (actionsAsset == null) {
            Debug.LogError("InputActionFile not Loaded in ActionItem.cs");
            return;
        }

        for (int i = 0; i < actionsAsset.actionMaps.Count; i++) {
            if(!actionsAsset.actionMaps[i].name.Substring(0, actionsAsset.actionMaps[i].name.IndexOf('-')).Equals(actionGroup)){
                continue;
            }
            if (actionsAsset.actionMaps[i].name.Substring(actionsAsset.actionMaps[i].name.IndexOf('-')+1).Equals(configName)) {
                actionsAsset.actionMaps[i].Enable();
                // UPDATE Configuration Details
                HashSet<string> deviceNameSet = new HashSet<string>();
                for (int j = 0; j < actionsAsset.actionMaps[i].bindings.Count; j++) {
                    string deviceName = actionsAsset.actionMaps[i].bindings[j].path;
                    if (deviceName.Contains("<")){
                        deviceName = deviceName.Substring(deviceName.IndexOf("<")+1, deviceName.IndexOf(">")-1);
                        deviceNameSet.Add(deviceName);
                    }
                }
                currentConfigurationDevices = new string[deviceNameSet.Count];
                deviceNameSet.CopyTo(currentConfigurationDevices);
                InputSystem.onDeviceChange += (UnityEngine.InputSystem.InputDevice device, InputDeviceChange change) => {
                    UpdateConnectedDevices(currentConfigurationDevices);
                };
                UpdateConnectedDevices(currentConfigurationDevices);
            } else {
                actionsAsset.actionMaps[i].Disable();
            }
        }

        Canvas.ForceUpdateCanvases();

        // Reset Binding Details Container
        foreach (Transform child in bindingInfoContainer.transform) {
            GameObject.Destroy(child.gameObject);
        }

        // For each Action, provide Binding Details in this form ACTION: Gamepad/leftClick
        // There should be one binding per action, and they should be instances of bindingInfoPrefab
        // inserted in bindingInfoContainer
        HashSet<string> actionsDisplayed = new HashSet<string>();
        for (int i = 0; i < actionsAsset.actionMaps.Count; i++){
            if(!actionsAsset.actionMaps[i].name.Equals(actionGroup+"-"+configName)){
                continue;
            }
            for (int j = 0; j < actionsAsset.actionMaps[i].bindings.Count; j++){
                string bindingInfo = actionsAsset.actionMaps[i].bindings[j].action.ToUpper();
                if (actionsDisplayed.Contains(bindingInfo)) {
                    continue;
                }
                actionsDisplayed.Add(bindingInfo);
                bindingInfo+= ": ";
                bindingInfo+= actionsAsset.actionMaps[i].bindings[j].path;

                GameObject newItem = Instantiate(bindingInfoPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0 ,0));
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = bindingInfo;

                newItem.transform.SetParent(bindingInfoContainer, false);
                ReEnableAfterFrame(bindingInfoContainer.gameObject);
            }
            
        }

        // Change buttons colour to reflect active configuration
        Image[] buttons = configContainer.GetComponentsInChildren<Image>();
            for (int i = 0; i < buttons.Length; i++){
                if (buttons[i].name.Equals("ConfigurationsContainer")) {
                    continue;
                }
                if (buttons[i].GetComponentInChildren<TextMeshProUGUI>().text.Equals(configName)){
                    // 75 75 75
                    buttons[i].color = new Color(0.29f, 0.29f, 0.29f, 1f);
                } else {
                    // 36 36 36
                    buttons[i].color = new Color(0.14f, 0.14f, 0.14f, 1f);
                }
            }

        // Update expandedHeight based on Extras height
        resizeExtrasWindow();
        Canvas.ForceUpdateCanvases();
    }

    void UpdateConnectedDevices(string[] currentConfigurationDevices) {
        if (currentConfigurationDevices == null){
            return;
        }
        if (configDetailsContainer == null){
            return;
        }

        // Reset Configuration Details Container
                foreach (Transform child in configDetailsContainer.transform) {
                    GameObject.Destroy(child.gameObject);
                }

                // Populate Configuration Details Container
                for (int k = 0; k < currentConfigurationDevices.Length; k++){
                    GameObject newItem = Instantiate(configDetailsItemPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0 ,0));
                    string deviceName = currentConfigurationDevices[k];
                    newItem.GetComponentInChildren<TextMeshProUGUI>().text = deviceName;

                    for (int f = 0; f < InputSystem.devices.Count; f++){
                        string connectedDeviceName = "";
                        if (InputSystem.devices[f] is Gamepad){
                            connectedDeviceName = "Gamepad";
                        } else if (InputSystem.devices[f].name.Contains("XR") && InputSystem.devices[f].name.Contains("Controller")) {
                            connectedDeviceName = "XRController";
                        } else {
                            connectedDeviceName = InputSystem.devices[f].name;
                        }

                        if (connectedDeviceName.Equals(deviceName)){
                            newItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Connected";
                            newItem.GetComponentsInChildren<TextMeshProUGUI>()[1].color = Color.green;
                        }
                    }

                    var inputDevices = new List<UnityEngine.XR.InputDevice>();
                    UnityEngine.XR.InputDevices.GetDevices(inputDevices);
                    // Debug.Log(inputDevices.Count);
                    Debug.Log("Device name:" + deviceName);

                    foreach (var device in inputDevices)
                    {
                        string connectedDeviceName = "";
                        // Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
                        // Debug.Log("Is device running? " + device.subsystem.running);
                        if (device.name.Contains("Controller")) {
                            connectedDeviceName = "XRController";
                        } else if (device.name.Contains("Head") || device.name.Contains("HMD"))
                        {
                            connectedDeviceName = "XRHMD";
                        }

                        if (connectedDeviceName.Equals(deviceName))
                        {
                            newItem.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Connected";
                            newItem.GetComponentsInChildren<TextMeshProUGUI>()[1].color = Color.green;
                        }
                    }

                    newItem.transform.SetParent(configDetailsContainer, false);
                    ReEnableAfterFrame(configDetailsContainer.gameObject);
                }

                Canvas.ForceUpdateCanvases();
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= (UnityEngine.InputSystem.InputDevice device, InputDeviceChange change) => {
            UpdateConnectedDevices(currentConfigurationDevices);
        };
    }

    //Reusable, just put a gameobject that's part of the layoutgroup and needs resetting visually
    IEnumerator ReEnableAfterFrame(GameObject theObject)
    {
        VerticalLayoutGroup layoutGroup = theObject.GetComponent<VerticalLayoutGroup>();
        yield return new WaitForEndOfFrame();

        layoutGroup.enabled = false;

        layoutGroup.CalculateLayoutInputVertical();

        LayoutRebuilder.ForceRebuildLayoutImmediate(theObject.GetComponent<RectTransform>());

        layoutGroup.enabled = true;
    }
}
