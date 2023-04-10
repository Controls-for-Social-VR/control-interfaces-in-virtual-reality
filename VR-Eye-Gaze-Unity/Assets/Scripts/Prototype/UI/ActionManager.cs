using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    Transform mainContainer;

    [SerializeField]
    InputActionAsset actionsAsset;
    [SerializeField]
    GameObject prefabActionGroupItem;
    
    // Start is called before the first frame update
    void Start()
    {
        mainContainer = GetComponent<Transform>();

        // Get Action Group Names, insert them in TMP Text component, 
        // and instantiate it as a child of MainContainer

        // Make it refresh everytime ProrotypeActions changes
        //InputActionMap[] actionGroups = actionsContainer.asset.actionMaps.ToArray();
        string[] actionGroupNames = getActionMapGroupNames(actionsAsset);
        for(int i = 0; i < actionGroupNames.Length; i++) {
            //Debug.Log("CURRENT ACTION GROUP: " + actionGroupNames[i] + " and current IDX: " + i);
            GameObject newItem = Instantiate(prefabActionGroupItem, new Vector3(0, 0, 0), new Quaternion(0, 0, 0 ,0));
            newItem.GetComponent<ActionItem>().FeedActionItem(actionGroupNames[i], actionsAsset);
            newItem.transform.SetParent(mainContainer, false);
        }
        
    }

    string[] getActionMapGroupNames (InputActionAsset inputActionsObj) {
        HashSet<string> actionMapGroupSet = new HashSet<string>();
        
        for(int i = 0; i < inputActionsObj.actionMaps.Count; i++) {
            string groupName = inputActionsObj.actionMaps[i].name;
            actionMapGroupSet.Add(groupName.Substring(0, groupName.IndexOf('-')));
        }

        string[] actionMapGroupSetArr = new string[actionMapGroupSet.Count];
        actionMapGroupSet.CopyTo(actionMapGroupSetArr);

        return actionMapGroupSetArr;
    }
}
