using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class UpdateEventSystem : MonoBehaviour
{
    InputSystemUIInputModule eventManager;

    // Start is called before the first frame update
    void Start()
    {
        eventManager = GetComponent<InputSystemUIInputModule>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
