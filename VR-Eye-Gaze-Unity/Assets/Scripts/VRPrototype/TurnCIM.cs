using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class TurnCIM : MonoBehaviour
{
    public ActionBasedContinuousTurnProvider turnProvider;
    public ActionBasedContinuousTurnProviderCIM turnProviderCIM;

    float sensitivityX = 80f;
    float sensitivityY = 80f;

    float yRotation = 0f;
    public Transform playerBody;
    public XROrigin XROrigin;

    // Update is called once per frame
    void Update()
    {
        if (turnProvider.rightHandTurnAction.action.actionMap.name.Contains("Gamepad"))
        {
            turnProvider.enabled = false;

            /*float valX = playerBody.rotation.x;
            float valY = playerBody.rotation.y;


            float rotationY = turnProvider.rightHandTurnAction.action.ReadValue<Vector2>().y * sensitivityX * Time.deltaTime;
            float rotationX = turnProvider.rightHandTurnAction.action.ReadValue<Vector2>().x * sensitivityY * Time.deltaTime;

            xRotation -= rotationY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation += rotationX;
*/
            // XROrigin. = Quaternion.Euler(xRotation, yRotation, 0);
            //XROrigin.RotateAroundCameraPosition(XROrigin.Camera.transform.right, )

            //XROrigin.RotateAroundCameraUsingOriginUp(turnAmount);
            if (turnProvider.rightHandTurnAction.action.ReadValue<Vector2>() != Vector2.zero)
            {
                var cardinal = CardinalUtility.GetNearestCardinal(turnProvider.rightHandTurnAction.action.ReadValue<Vector2>());

                switch (cardinal)
                {
                    case Cardinal.North:
                    case Cardinal.South:
                        float valueY = turnProvider.rightHandTurnAction.action.ReadValue<Vector2>().magnitude * (Mathf.Sign(turnProvider.rightHandTurnAction.action.ReadValue<Vector2>().y) * sensitivityY * Time.deltaTime);
                        valueY = -valueY;
                        yRotation += valueY;
                        
                        if (yRotation + valueY >= 90f || yRotation <= -90f)
                        {
                            Debug.Log("THE UP DOWN ROTATION IS OUT OF BOUNDS!!!!!!");
                            valueY = 0f;
                        }
                        XROrigin.RotateAroundCameraPosition(XROrigin.Camera.transform.right, valueY);
                        break;
                    case Cardinal.East:
                    case Cardinal.West:
                        float valueX = turnProvider.rightHandTurnAction.action.ReadValue<Vector2>().magnitude * (Mathf.Sign(turnProvider.rightHandTurnAction.action.ReadValue<Vector2>().x) * sensitivityX * Time.deltaTime);
                        XROrigin.RotateAroundCameraPosition(Vector3.up, valueX);
                        break;
                }

            }

        }
        else
        {
            turnProvider.enabled = true;
        }
    }
}