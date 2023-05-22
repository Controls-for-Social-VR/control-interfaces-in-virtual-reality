using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TurnCIM : MonoBehaviour
{
    public ActionBasedContinuousTurnProvider turnProvider;
    public ActionBasedContinuousTurnProviderCIM turnProviderCIM;
    public float rotationSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        if (turnProvider.rightHandTurnAction.action.actionMap.name.Contains("Gamepad"))
        {
            turnProvider.enabled = false;

            float rotX = turnProvider.rightHandTurnAction.action.ReadValue<Vector2>().x;
            float rotY = turnProvider.rightHandTurnAction.action.ReadValue<Vector2>().y;

            transform.Rotate(rotationSpeed * rotX, rotationSpeed * rotY, 0.0f);
        } else
        {
            turnProvider.enabled = true;
        }
    }
}
